import React, {useMemo, useState} from "react";
import TreeView from 'devextreme-react/tree-view';
import {formatDate} from "devextreme/localization";
import CustomStore from "devextreme/data/custom_store";
import {getOrganizations} from "../services/getOrganizations";
import {getUsers} from "../services/getUsers";
import {getCardiographs} from "../services/getCardiographs";
import {getCardiograms} from "../services/getCardiograms";
import {getEntireCardiogram} from "../services/getEntireCardiogram";
import AdditionalInformation from "./AdditionalInformation";
import {Button} from "devextreme-react";
import './MainDataView.scss'
import store from "../redux/store";
import {logout} from "../redux/reducers/user";
import {useSelector} from "react-redux";
import {Navigate} from "react-router-dom";

export default function MainDataView() {
    const [currentItem, setCurrentItem] = useState(null);
    const [itemVisible, setItemVisible] = useState(null);

    const user = useSelector(state => state.user);

    const handleItemClick = async (e) => {
        const { itemData } = e;
        // Если у узла есть дочерние элементы - раскрываем/закрываем
        if (itemData.hasItems) {
            await e.component.expandItem(itemData.id);
        }

        // Если это кардиограмма - показываем дополнительную информацию
        else {
            const [nodeType, guid] = itemData.id.split('|');
            if (nodeType === 'cardiogram') {
                try {
                    const request = await getEntireCardiogram(guid);
                    setCurrentItem(request);
                    setItemVisible(true);
                } catch (error) {
                    console.error('Ошибка загрузки кардиограммы:', error);
                }
            }
        }
    };

    const formatDateTime = (isoString) => {
        const date = new Date(isoString);
        return formatDate(date, "dd.MM.yyyy HH:mm") || "Не указано";
    };

    const dataSource = useMemo(() => new CustomStore({
        key: 'id',
        parentIdExpr: 'parentId',
        hasItemsExpr: 'hasItems',

        async load(loadOptions) {
            const parentNode = loadOptions?.filter[1] || null;

            if (!parentNode) {
                const organizations = await getOrganizations();
                return organizations.successEntity.map(organization => ({
                    id: `organization|${organization.organizationUuid}`,
                    text: organization.name,
                    parentId: null,
                    hasItems: true
                }));
            }

            const [nodeType, guid] = parentNode.split('|');

            try {
                switch(nodeType) {
                    case 'organization':
                        const users = await getUsers(guid);
                        return users.map(user => ({
                            id: `user|${user.userUuid}`,
                            text: user.fullName,
                            parentId: parentNode,
                            hasItems: true
                        }));
                    case 'user':
                        const cardiograph = await getCardiographs(guid);
                        return cardiograph.map(graph => ({
                            id: `cardiograph|${graph.serialNumber}|${Date.now()}`,
                            text:  graph.cardiographName,
                            parentId: parentNode,
                            hasItems: true
                        }));
                    case 'cardiograph':
                        const cardiogram = await getCardiograms(guid);
                        return cardiogram.map(gram => ({
                            id: `cardiogram|${gram.cardiogramUuid}|${Date.now()}`,
                            text: "Кардиограмма от " + formatDateTime(gram.measurementTime),
                            parentId: parentNode,
                            hasItems: false,
                            data: gram
                        }));

                    default:
                        return [];
                }
            } catch (error) {
                console.error('Ошибка загрузки:', error);
                return [];
            }
        }
    }), [getUsers, getCardiographs, getCardiograms]);

    function Logout(){
        store.dispatch(logout())
    }

    if (!user.isLoggedIn){
        return <Navigate to="/login" />
    }

    return (<div className="form">
            {currentItem && <AdditionalInformation data={currentItem} visible={itemVisible} setVisible={setItemVisible}/>}

            <header className={"top-menu"} >
                <div>
                    {user.user.login} {user.user.fullName}
                    <Button type={"default"}
                        width={"max-content"}
                    >
                        Добавить
                    </Button>
                </div>
                <Button type={"danger"} text={"Выход"} onClick={Logout} />

            </header>

            <div className="form">
                <TreeView
                    dataSource={dataSource}
                    dataStructure="plain"
                    rootValue={null}
                    displayExpr="text"
                    keyExpr="id"
                    parentIdExpr="parentId"
                    hasItemsExpr="hasItems"
                    virtualModeEnabled={true}
                    expandNodesRecursive={false}
                    onItemClick={handleItemClick}
                />
            </div>
        </div>

    )
}
