import React, { useMemo, useState } from "react";
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
import { Button } from "devextreme-react";
import "./MainDataView.scss";
import store from "../redux/store";
import { logout } from "../redux/reducers/user";
import { useSelector } from "react-redux";
import { Navigate } from "react-router-dom";
import CardiogramCreation from "./CreateCardiogram/CardiogramCreation";

export default function MainDataView() {
    const [currentItem, setCurrentItem] = useState(null);
    const [itemVisible, setItemVisible] = useState(null);
    const [isAdd, setIsAdd] = useState(false);

    const [contextMenuVisible, setContextMenuVisible] = useState(false);
    const [contextMenuTarget, setContextMenuTarget] = useState(null);
    const [contextMenuItem, setContextMenuItem] = useState(null);

    const user = useSelector((state) => state.user);

    // Контекстное меню с вариантами действий
    const contextMenuItems = [
        { text: "Посмотреть", action: "view" },
        { text: "Изменить", action: "edit" },
        { text: "Удалить", action: "delete" }
    ];

    const handleContextMenuItemClick = (e) => {
        const action = e.itemData.action;
        console.log("Context menu action:", action, "for item:", contextMenuItem);
        // Здесь можно реализовать логику для каждого действия
        // Например, если action === "view", то открыть окно с информацией
        // Если "edit" – открыть форму редактирования, и т.д.
        setContextMenuVisible(false);
    };

    const handleItemContextMenu = (e) => {
        // Отменяем стандартное контекстное меню браузера
        e.event.preventDefault();
        // Сохраняем данные узла, на который был совершен правый клик
        setContextMenuItem(e.itemData);
        // Сохраняем цель для отображения меню (DOM-элемент, на котором произошёл клик)
        setContextMenuTarget(e.event.target);
        // Отображаем контекстное меню
        setContextMenuVisible(true);
    };

    const handleItemClick = async (e) => {
        const { itemData } = e;
        // Если у узла есть дочерние элементы - раскрываем/закрываем
        if (itemData.hasItems) {
            await e.component.expandItem(itemData.id);
        }
        // Если это кардиограмма - показываем дополнительную информацию
        else {
            const [nodeType, guid] = itemData.id.split("|");
            if (nodeType === "cardiogram") {
                try {
                    const request = await getEntireCardiogram(guid);
                    setCurrentItem(request);
                    setItemVisible(true);
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
                        const organizations = await getOrganizations();
                        return organizations.successEntity.map((organization) => ({
                            id: `organization|${organization.organizationUuid}`,
                            text: organization.name,
                            parentId: null,
                            hasItems: true
                        }));
                    }
                    const [nodeType, guid] = parentNode.split("|");
                    try {
                        switch (nodeType) {
                            case "organization":
                                const users = await getUsers(guid);
                                return users.map((user) => ({
                                    id: `user|${user.userUuid}`,
                                    text: user.fullName,
                                    parentId: parentNode,
                                    hasItems: true
                                }));
                            case "user":
                                const cardiograph = await getCardiographs(guid);
                                return cardiograph.successEntity.map((graph) => ({
                                    id: `cardiograph|${graph.serialNumber}|${Date.now()}`,
                                    text: graph.cardiographName,
                                    parentId: parentNode,
                                    hasItems: true
                                }));
                            case "cardiograph":
                                const cardiogram = await getCardiograms(guid);
                                return cardiogram.map((gram) => ({
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
                />
            )}
            {isAdd && <CardiogramCreation isVisible={isAdd} setIsVisible={setIsAdd} />}
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
            {/* Контекстное меню */}
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
