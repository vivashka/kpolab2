import {TextArea, TextBox} from "devextreme-react";

export function ResultView({ result, isModify, onFieldChange }) {
    if (!result) return <div>Нет данных о результатах</div>;

    const handleChange = (field, value) => {
        if (onFieldChange) {
            onFieldChange({ ...result, [field]: value });
        }
    };

    return (
        <div className="result-container" style={{ padding: "20px", maxWidth: "600px" }}>
            <h1 style={{ marginBottom: "20px" }}>Результаты исследования</h1>

            <div className="section">
                <h2>Основные данные</h2>
                <div><strong>Главный диагноз:</strong>
                    {isModify ? (
                        <TextBox
                            value={result.diagnosisMain}
                            onValueChanged={(e) => handleChange("diagnosisMain", e.value)}
                        />
                    ) : (
                        result.diagnosisMain || "Не указан"
                    )}
                </div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Описание</h2>
                <div style={{ whiteSpace: "pre-wrap" }}>
                    {isModify ? (
                        <TextArea
                            value={result.description}
                            onValueChanged={(e) => handleChange("description", e.value)}
                            multiline
                        />
                    ) : (
                        result.description || "Описание отсутствует"
                    )}
                </div>
            </div>
        </div>
    );
}
