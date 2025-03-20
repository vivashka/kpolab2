import { TextBox } from "devextreme-react";
import { useEffect, useState } from "react";

export function OrganizationView({ organization, isModify, isSave }) {
    if (!organization) return <div>Нет данных об организации</div>;

    // Локальное состояние для редактируемых данных
    const [newData, setNewData] = useState(organization);

    // Обновляем локальное состояние, если изменился объект organization из пропсов
    useEffect(() => {
        setNewData(organization);
    }, [organization]);

    // Обработчик изменения поля
    const handleChange = (field, value) => {
        setNewData((prev) => ({ ...prev, [field]: value }));
    };

    // При завершении редактирования сохраняем данные
    // useEffect(() => {
    //     if (!isModify && isSave) {
    //         async function pushData() {
    //             console.log("Отправка данных:", newData);
    //             const response = await SaveOrganization(newData);
    //             if (response.isSuccess) {
    //                 console.log("Успешно обновлено");
    //                 setNewData(response.successEntity);
    //             } else {
    //                 store.dispatch(showModal(response?.errorEntity));
    //             }
    //         }
    //         pushData();
    //     }
    // }, [isModify, isSave]);

    return (
        <div className="organization-container" style={{ padding: "20px", maxWidth: "600px" }}>
            <h1 style={{ marginBottom: "20px" }}>Информация об организации</h1>

            <div className="section">
                <h2>Основная информация</h2>
                <div>
                    <strong>UUID организации:</strong> {organization.organizationUuid}
                </div>
                <div>
                    <strong>Название:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.name}
                            onValueChanged={(e) => handleChange("name", e.value)}
                        />
                    ) : (
                        newData.name || "Не указано"
                    )}
                </div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Контактные данные</h2>
                <div>
                    <strong>Номер ССМП:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.ssmpNumber}
                            onValueChanged={(e) => handleChange("ssmpNumber", e.value)}
                        />
                    ) : (
                        newData.ssmpNumber
                    )}
                </div>
                <div>
                    <strong>Адрес ССМП:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.ssmpAdress}
                            onValueChanged={(e) => handleChange("ssmpAdress", e.value)}
                        />
                    ) : (
                        newData.ssmpAdress || "Не указан"
                    )}
                </div>
                <div>
                    <strong>Контактное лицо:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.phoneContactName}
                            onValueChanged={(e) => handleChange("phoneContactName", e.value)}
                        />
                    ) : (
                        newData.phoneContactName || "Не указано"
                    )}
                </div>
                <div>
                    <strong>Номер телефона:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.phoneNumber}
                            onValueChanged={(e) => handleChange("phoneNumber", e.value)}
                        />
                    ) : (
                        newData.phoneNumber
                    )}
                </div>
            </div>
        </div>
    );
}
