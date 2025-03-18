import {TextArea, TextBox} from "devextreme-react";
import {useEffect, useState} from "react";
import {SaveResult} from "../services/SaveResult";

export function ResultView({ result, isModify }) {
    if (!result) return <div>Нет данных о результатах</div>;

    const [newData, setNewData] = useState(result || {});

    const handleChange = (field, value) => {
        setNewData({ ...result, [field]: value });
    };

    useEffect(() => {

        if (!isModify) {
            async function pushData() {
                console.log(newData)
                const response = await SaveResult(newData);
                if (response.cardiogramUuid) {
                    console.log("Успешно обновлено");
                    setNewData(response)
                }
            }
            pushData();
        }
    }, [isModify]);

    return (
        <div className="result-container" style={{ padding: "20px", maxWidth: "600px" }}>
            <h1 style={{ marginBottom: "20px" }}>Результаты исследования</h1>

            <div className="section">
                <h2>Основные данные</h2>
                <div><strong>Главный диагноз:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.diagnosisMain}
                            onValueChanged={(e) => handleChange("diagnosisMain", e.value)}
                        />
                    ) : (
                        newData.diagnosisMain || "Не указан"
                    )}
                </div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Описание</h2>
                <div style={{ whiteSpace: "pre-wrap" }}>
                    {isModify ? (
                        <TextArea
                            value={newData.description}
                            onValueChanged={(e) => handleChange("description", e.value)}
                            multiline
                        />
                    ) : (
                        newData.description || "Описание отсутствует"
                    )}
                </div>
            </div>
        </div>
    );
}
