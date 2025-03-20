import React, { useMemo, useState, useRef } from "react";
import TreeView from "devextreme-react/tree-view";
import { ContextMenu } from "devextreme-react/context-menu";
import { formatDate } from "devextreme/localization";
import CustomStore from "devextreme/data/custom_store";
import { getOrganizations } from "../services/getOrganizations";
import { getUsers } from "../services/getUsers";
import { getCardiographs } from "../services/getCardiographs";
import { getCardiograms } from "../services/getCardiograms";
import { getEntireCardiogram } from "../services/getEntireCardiogram";
import AdditionalInformation from "./AdditionalInformation";
import { Button, Popup } from "devextreme-react";
import "./MainDataView.scss";
import store from "../redux/store";
import { logout } from "../redux/reducers/user";
import { useSelector } from "react-redux";
import { Navigate } from "react-router-dom";
import CardiogramCreation from "./CreateCardiogram/CardiogramCreation";
import { OrganizationView } from "./CreateCardiogram/OrganizationView";
import { UserView } from "./UserView";
import { CardiographView } from "./CardiographView";
import { deleteCardiogram } from "../services/deleteCardiogram";
import { showModal } from "../redux/reducers/error";

export default function MainDataView() {
    const [currentItem, setCurrentItem] = useState(null);
    const [itemVisible, setItemVisible] = useState(false);
    const [isAdd, setIsAdd] = useState(false);

    const [contextMenuVisible, setContextMenuVisible] = useState(false);
    const [contextMenuTarget, setContextMenuTarget] = useState(null);
    const [contextMenuItem, setContextMenuItem] = useState(null);
    const [modify, setModify] = useState(false);
    const [separate, setSeparate] = useState(null);
    const [data, setData] = useState(null);
    const [isDataVisible, setIsDataVisible] = useState(false);
    // Счетчик для принудительного обновления дерева
    const [dataVersion, setDataVersion] = useState(0);

    const treeRef = useRef(null);

    const user = useSelector((state) => state.user);

    const contextMenuItems = [
        { text: "Посмотреть", action: "view" },
        { text: "Изменить", action: "edit" },
        { text: "Удалить", action: "delete" }
    ];

    async function DataView(id, nodeType, isEdit) {
        switch (nodeType) {
            case "organization": {
                const orgRes = await getOrganizations();
                const organization = orgRes.successEntity.find(x => x.organizationUuid === id);
                return organization ? (
                    <OrganizationView organization={organization} isModify={isEdit} isSave={false} />
                ) : (
                    <div>Организация не найдена</div>
                );
            }
            case "user": {
                const usersRes = await getUsers("");
                const foundUser = usersRes.successEntity.find(x => x.userUuid === id);
                return foundUser ? (
                    <UserView user={foundUser} isModify={isEdit} isSave={false} />
                ) : (
                    <div>Пользователь не найден</div>
                );
            }
            case "cardiograph": {
                const cardioRes = await getCardiographs("");
                const cardiograph = cardioRes.successEntity.find(x => x.serialNumber === id);
                return cardiograph ? (
                    <CardiographView cardiograph={cardiograph} isModify={isEdit} isSave={false} />
                ) : (
                    <div>Кардиограф не найден</div>
                );
            }
            default:
                return <div>Нет данных</div>;
        }
    }

    const handleContextMenuItemClick = async (e) => {
        const action = e.itemData.action;
        console.log("Context menu action:", action, "for item:", contextMenuItem);
        const [nodeType, guid] = contextMenuItem.id.split("|");

        if (["organization", "user", "cardiograph"].includes(nodeType)) {
            const isEdit = action === "edit";
            setSeparate(nodeType);
            setIsDataVisible(true);
            const viewData = await DataView(guid, nodeType, isEdit);
            setData(viewData);
            setModify(isEdit);
        }

        if (nodeType === "cardiogram") {
            if (action === "delete") {
                const requestBody = { guid: guid };
                const response = await deleteCardiogram(requestBody);
                if (response.isSuccess) {
                    // Обновляем дерево путем изменения ключа (dataVersion)
                    setData(null);
                    setDataVersion(prev => prev + 1);
                    setContextMenuVisible(false);
                    return;
                } else {
                    store.dispatch(showModal(response.errorEntity));
                    setContextMenuVisible(false);
                    return;
                }
            }
            const request = await getEntireCardiogram(guid);
            setCurrentItem(request);
            setItemVisible(true);
            setModify(action === "edit");
        }
        setContextMenuVisible(false);
    };

    const handleItemContextMenu = (e) => {
        e.event.preventDefault();
        setContextMenuItem(e.itemData);
        setContextMenuTarget(e.event.target);
        setContextMenuVisible(true);
    };

    const handleItemClick = async (e) => {
        const { itemData } = e;
        if (itemData.hasItems) {
            await e.component.expandItem(itemData.id);
        } else {
            const [nodeType, guid] = itemData.id.split("|");
            if (nodeType === "cardiogram") {
                try {
                    const request = await getEntireCardiogram(guid);
                    setCurrentItem(request);
                    setItemVisible(true);
                    setModify(false);
                } catch (error) {
                    console.error("Ошибка загрузки кардиограммы:", error);
                }
            }
        }
    };

    const formatDateTime = (isoString) => {
        const date = new Date(isoString);
        return formatDate(date, "dd.MM.yyyy HH:mm") || "Не указано";
    };

    const dataSource = useMemo(
        () =>
            new CustomStore({
                key: "id",
                parentIdExpr: "parentId",
                hasItemsExpr: "hasItems",
                async load(loadOptions) {
                    const parentNode = loadOptions?.filter[1] || null;
                    if (!parentNode) {
                        const orgRes = await getOrganizations();
                        return orgRes.successEntity.map((organization) => ({
                            id: `organization|${organization.organizationUuid}`,
                            text: organization.name,
                            parentId: null,
                            hasItems: true
                        }));
                    }
                    const [nodeType, guid] = parentNode.split("|");
                    try {
                        switch (nodeType) {
                            case "organization": {
                                const usersRes = await getUsers(guid);
                                return usersRes.successEntity.map((user) => ({
                                    id: `user|${user.userUuid}`,
                                    text: user.fullName,
                                    parentId: parentNode,
                                    hasItems: true
                                }));
                            }
                            case "user": {
                                const cardioRes = await getCardiographs(guid);
                                return cardioRes.successEntity.map((graph) => ({
                                    id: `cardiograph|${graph.serialNumber}|${Date.now()}`,
                                    text: graph.cardiographName,
                                    parentId: parentNode,
                                    hasItems: true
                                }));
                            }
                            case "cardiograph": {
                                const cardiogram = await getCardiograms(guid);
                                return cardiogram.map((gram) => ({
                                    id: `cardiogram|${gram.cardiogramUuid}|${Date.now()}`,
                                    text: "Кардиограмма от " + formatDateTime(gram.measurementTime),
                                    parentId: parentNode,
                                    hasItems: false,
                                    data: gram
                                }));
                            }
                            default:
                                return [];
                        }
                    } catch (error) {
                        console.error("Ошибка загрузки:", error);
                        return [];
                    }
                }
            }),
        []
    );

    function Logout() {
        store.dispatch(logout());
    }

    if (!user.isLoggedIn) {
        return <Navigate to="/login" />;
    }

    return (
        <div className="form">
            {currentItem && (
                <AdditionalInformation
                    data={currentItem}
                    visible={itemVisible}
                    setVisible={setItemVisible}
                    modify={modify}
                    setModify={setModify}
                />
            )}
            <Popup
                visible={isDataVisible}
                onHiding={() => {
                    setIsDataVisible(false);
                    setData(null);
                }}
            >
                <div
                    style={{
                        height: "calc(100% - 50px)",
                        overflowY: "auto",
                        padding: "10px"
                    }}
                >
                    {data || <div>Загрузка...</div>}
                </div>
                <div className="additional-buttons">
                    <Button
                        className="button button-change"
                        text="Изменить"
                        onClick={() => setModify(true)}
                    />
                    <Button
                        className="button button-apply"
                        text="Сохранить"
                        onClick={() => setModify(false)}
                        disabled={!modify}
                    />
                </div>
            </Popup>
            {isAdd && (
                <CardiogramCreation isVisible={isAdd} setIsVisible={setIsAdd} />
            )}
            <header className="top-menu">
                <div>
                    {user.user.login} {user.user.fullName}
                    <Button
                        type="default"
                        width="max-content"
                        onClick={() => setIsAdd(true)}
                    >
                        Добавить
                    </Button>
                </div>
                <Button type="danger" text="Выход" onClick={Logout} />
            </header>
            <div className="form">
                <TreeView
                    ref={treeRef}
                    key={dataVersion}  // используем dataVersion как key для перерисовки
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
                    onItemContextMenu={handleItemContextMenu}
                />
            </div>
            <ContextMenu
                dataSource={contextMenuItems}
                target={contextMenuTarget}
                visible={contextMenuVisible}
                onItemClick={handleContextMenuItemClick}
                onHidden={() => setContextMenuVisible(false)}
            />
        </div>
    );
}
