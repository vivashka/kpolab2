import { formatDate } from "devextreme/localization"; // Или используйте другую библиотеку для форматирования дат

export default function CallView({ call }) {
    if (!call) return <div>Нет данных о вызове</div>;

    // Форматирование даты/времени
    const formatDateTime = (isoString) => {
        const date = new Date(isoString);
        return formatDate(date, "dd.MM.yyyy HH:mm") || "Не указано";
    };

    // Преобразование числового пола в текст
    const getGender = (sex) => {
        switch(sex) {
            case 0: return "Женский";
            case 1: return "Мужской";
            default: return "Не указан";
        }
    };

    return (
        <div className="call-container" style={{ padding: "20px", maxWidth: "600px" }}>
            <h1 style={{ marginBottom: "20px" }}>Детали вызова</h1>

            {/* Основная информация */}
            <div className="section">
                <h2>Общая информация</h2>
                <div><strong>Тип вызова:</strong> {call.callType || "Не указан"}</div>
                <div><strong>Приоритет:</strong> {call.priority}</div>
                <div><strong>Диагноз:</strong> {call.callDiagnosis || "Не указан"}</div>
            </div>

            {/* Информация о пациенте */}
            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Пациент</h2>
                <div><strong>ФИО:</strong> {[
                    call.patientSurname,
                    call.patientName,
                    call.patientPatronymic
                ].filter(Boolean).join(" ")}</div>
                <div><strong>Возраст:</strong> {call.patientAge}</div>
                <div><strong>Пол:</strong> {getGender(call.patientSex)}</div>
            </div>

            {/* Адресные данные */}
            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Локация</h2>
                <div><strong>Адрес:</strong> {[
                    `ул. ${call.street}`,
                    `д. ${call.streetNumber}`,
                    call.apartmentNumber && `кв. ${call.apartmentNumber}`,
                    call.entrance && `подъезд ${call.entrance}`
                ].filter(Boolean).join(", ")}</div>
                <div><strong>Адрес госпитализации:</strong> {call.hospitalizationAddress}</div>
            </div>

            {/* Временные метки */}
            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Временные отметки</h2>
                <div><strong>Получен:</strong> {formatDateTime(call.receiveTime)}</div>
                <div><strong>Передан:</strong> {formatDateTime(call.transferTime)}</div>
                <div><strong>Выезд:</strong> {formatDateTime(call.departureTime)}</div>
                <div><strong>Прибытие:</strong> {formatDateTime(call.arrivalTime)}</div>
            </div>

            {/* Дополнительная информация */}
            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Детали бригады</h2>
                <div><strong>Бригада №:</strong> {call.brigadeNumber}</div>
                <div><strong>Номер ССМП:</strong> {call.ssmpNumber}</div>
                <div><strong>Причина вызова:</strong> {call.reason}</div>
            </div>
        </div>
    );
}