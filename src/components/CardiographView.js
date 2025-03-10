export function CardiographView({ cardiograph }) {
    if (!cardiograph) return <div>Нет данных об оборудовании</div>;

    return (
        <div className="cardiograph-container" style={{ padding: "20px", maxWidth: "600px" }}>
            <h1 style={{ marginBottom: "20px" }}>Информация об оборудовании</h1>

            <div className="section">
                <h2>Идентификационные данные</h2>
                <div><strong>Серийный номер:</strong> {cardiograph.serialNumber}</div>
                <div><strong>Название модели:</strong> {cardiograph.cardiographName || "Не указано"}</div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Производитель</h2>
                <div><strong>Компания:</strong> {cardiograph.manufacturerName || "Не указано"}</div>
            </div>
        </div>
    );
}