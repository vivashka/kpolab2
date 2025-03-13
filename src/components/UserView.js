import { TextBox } from "devextreme-react";

export function UserView({ user, isModify }) {
    if (!user) return <div>Нет данных о пользователе</div>;

    const handleChange = (field, value) => {

    };

    return (
        <div className="user-container" style={{ padding: "20px", maxWidth: "600px" }}>
            <h1 style={{ marginBottom: "20px" }}>Редактирование пользователя</h1>

            <div className="section">
                <h2>Основные данные</h2>
                <div><strong>Табельный:</strong>
                    {isModify ? (
                        <TextBox value={user.login} onValueChanged={(e) => handleChange("login", e.value)} />
                    ) : (
                        user.login
                    )}
                </div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Контакты</h2>
                <div><strong>Телефон:</strong>
                    {isModify ? (
                        <TextBox value={user.phoneNumber} onValueChanged={(e) => handleChange("phoneNumber", e.value)} />
                    ) : (
                        user.phoneNumber || "Не указан"
                    )}
                </div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Информация</h2>
                <div><strong>ФИО:</strong>
                    {isModify ? (
                        <TextBox value={user.fullName} onValueChanged={(e) => handleChange("fullName", e.value)} />
                    ) : (
                        user.fullName
                    )}
                </div>
                <div><strong>Должность:</strong>
                    {isModify ? (
                        <TextBox value={user.appointment} onValueChanged={(e) => handleChange("appointment", e.value)} />
                    ) : (
                        user.appointment || "Не указана"
                    )}
                </div>
            </div>
        </div>
    );
}
