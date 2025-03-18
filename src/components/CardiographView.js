import { TextBox } from "devextreme-react";
import {useEffect, useState} from "react";
import {SaveCardiograph, saveCardiograph} from "../services/SaveCardiograph";

export function CardiographView({ cardiograph, isModify }) {
    if (!cardiograph) return <div>Нет данных об оборудовании</div>;

    const [newData, setNewData] = useState(cardiograph || {});

    const handleChange = (field, value) => {
        setNewData({ ...cardiograph, [field]: value });
    };

    useEffect(() => {

        if (!isModify) {
            async function pushData() {
                console.log(newData)
                const response = await SaveCardiograph(newData);
                if (response.cardiogramUuid) {
                    console.log("Успешно обновлено");
                    setNewData(response)
                }
            }
            pushData();
        }
    }, [isModify]);

    return (
        <div className="cardiograph-container" style={{ padding: "20px", maxWidth: "600px" }}>
            <h1 style={{ marginBottom: "20px" }}>Информация об оборудовании</h1>

            <div className="section">
                <h2>Идентификационные данные</h2>
                <div><strong>Серийный номер:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.serialNumber}
                            onValueChanged={(e) => handleChange("serialNumber", e.value)}
                        />
                    ) : (
                        newData.serialNumber
                    )}
                </div>
                <div><strong>Название модели:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.cardiographName}
                            onValueChanged={(e) => handleChange("cardiographName", e.value)}
                        />
                    ) : (
                        newData.cardiographName || "Не указано"
                    )}
                </div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Производитель</h2>
                <div><strong>Компания:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.manufacturerName}
                            onValueChanged={(e) => handleChange("manufacturerName", e.value)}
                        />
                    ) : (
                        newData.manufacturerName || "Не указано"
                    )}
                </div>
            </div>
        </div>
    );
}
