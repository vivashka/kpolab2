import { TextBox } from "devextreme-react";

export function CardiographView({ cardiograph, isModify, onFieldChange }) {
    if (!cardiograph) return <div>Нет данных об оборудовании</div>;

    const handleChange = (field, value) => {
        if (onFieldChange) {
            onFieldChange({ ...cardiograph, [field]: value });
        }
    };

    return (
        <div className="cardiograph-container" style={{ padding: "20px", maxWidth: "600px" }}>
            <h1 style={{ marginBottom: "20px" }}>Информация об оборудовании</h1>

            <div className="section">
                <h2>Идентификационные данные</h2>
                <div><strong>Серийный номер:</strong>
                    {isModify ? (
                        <TextBox
                            value={cardiograph.serialNumber}
                            onValueChanged={(e) => handleChange("serialNumber", e.value)}
                        />
                    ) : (
                        cardiograph.serialNumber
                    )}
                </div>
                <div><strong>Название модели:</strong>
                    {isModify ? (
                        <TextBox
                            value={cardiograph.cardiographName}
                            onValueChanged={(e) => handleChange("cardiographName", e.value)}
                        />
                    ) : (
                        cardiograph.cardiographName || "Не указано"
                    )}
                </div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Производитель</h2>
                <div><strong>Компания:</strong>
                    {isModify ? (
                        <TextBox
                            value={cardiograph.manufacturerName}
                            onValueChanged={(e) => handleChange("manufacturerName", e.value)}
                        />
                    ) : (
                        cardiograph.manufacturerName || "Не указано"
                    )}
                </div>
            </div>
        </div>
    );
}
