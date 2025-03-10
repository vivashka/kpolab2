export function ResultView({ result }) {
    if (!result) return <div>Нет данных о результатах</div>;

    return (
        <div className="result-container" style={{ padding: "20px", maxWidth: "600px" }}>
            <h1 style={{ marginBottom: "20px" }}>Результаты исследования</h1>

            <div className="section">
                <h2>Основные данные</h2>
                <div><strong>Главный диагноз:</strong> {result.diagnosisMain || "Не указан"}</div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Описание</h2>
                <div style={{ whiteSpace: "pre-wrap" }}>
                    {result.description || "Описание отсутствует"}
                </div>
            </div>
        </div>
    );
}