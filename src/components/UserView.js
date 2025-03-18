import { TextBox } from "devextreme-react";
import {useEffect, useState} from "react";
import {SaveResult} from "../services/SaveResult";
import {SaveUser} from "../services/SaveUser";

export function UserView({ user, isModify }) {
    if (!user) return <div>Нет данных о пользователе</div>;

    const [newData, setNewData] = useState(user || {});

    const handleChange = (field, value) => {
        setNewData({ ...user, [field]: value });
    };

    useEffect(() => {

        if (!isModify) {
            async function pushData() {
                console.log(newData)
                const response = await SaveUser(newData);
                if (response.cardiogramUuid) {
                    console.log("Успешно обновлено");
                    setNewData(response)
                }
            }
            pushData();
        }
    }, [isModify]);

    return (
        <div className="user-container" style={{ padding: "20px", maxWidth: "600px" }}>
            <h1 style={{ marginBottom: "20px" }}>Редактирование пользователя</h1>

            <div className="section">
                <h2>Основные данные</h2>
                <div><strong>Табельный:</strong>
                    {isModify ? (
                        <TextBox value={newData.login} onValueChanged={(e) => handleChange("login", e.value)} />
                    ) : (
                        user.login
                    )}
                </div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Контакты</h2>
                <div><strong>Телефон:</strong>
                    {isModify ? (
                        <TextBox value={newData.phoneNumber} onValueChanged={(e) => handleChange("phoneNumber", e.value)} />
                    ) : (
                        newData.phoneNumber || "Не указан"
                    )}
                </div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Информация</h2>
                <div><strong>ФИО:</strong>
                    {isModify ? (
                        <TextBox value={newData.fullName} onValueChanged={(e) => handleChange("fullName", e.value)} />
                    ) : (
                        newData.fullName
                    )}
                </div>
                <div><strong>Должность:</strong>
                    {isModify ? (
                        <TextBox value={newData.appointment} onValueChanged={(e) => handleChange("appointment", e.value)} />
                    ) : (
                        newData.appointment || "Не указана"
                    )}
                </div>
            </div>
        </div>
    );
}
