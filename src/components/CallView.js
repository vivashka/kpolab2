import { formatDate } from "devextreme/localization";
import { TextBox, SelectBox, DateBox } from "devextreme-react";

export default function CallView({ call, isModify, onFieldChange }) {
    if (!call) return <div>Нет данных о вызове</div>;

    const formatDateTime = (isoString) => {
        const date = new Date(isoString);
        return formatDate(date, "dd.MM.yyyy HH:mm") || "Не указано";
    };

    const handleChange = (field, value) => {
        if (onFieldChange) {
            onFieldChange({ ...call, [field]: value });
        }
    };

    const getGender = (sex) => {
        switch(sex) {
            case 0: return "Женский";
            case 1: return "Мужской";
            default: return "Не указан";
        }
    };

    const genderOptions = [
        { value: 0, text: "Женский" },
        { value: 1, text: "Мужской" }
    ];

    return (
        <div className="call-container" style={{ padding: "20px", maxWidth: "600px" }}>
            <h1 style={{ marginBottom: "20px" }}>Детали вызова</h1>

            <div className="section">
                <h2>Общая информация</h2>
                <div><strong>Тип вызова:</strong>
                    {isModify ? (
                        <TextBox
                            value={call.callType}
                            onValueChanged={(e) => handleChange("callType", e.value)}
                        />
                    ) : (
                        call.callType || "Не указан"
                    )}
                </div>
                <div><strong>Приоритет:</strong>
                    {isModify ? (
                        <TextBox
                            value={call.priority}
                            onValueChanged={(e) => handleChange("priority", e.value)}
                        />
                    ) : (
                        call.priority
                    )}
                </div>
                <div><strong>Диагноз:</strong>
                    {isModify ? (
                        <TextBox
                            value={call.callDiagnosis}
                            onValueChanged={(e) => handleChange("callDiagnosis", e.value)}
                        />
                    ) : (
                        call.callDiagnosis || "Не указан"
                    )}
                </div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Пациент</h2>
                <div><strong>ФИО:</strong>
                    {isModify ? (
                        <>
                            <TextBox
                                value={call.patientSurname}
                                onValueChanged={(e) => handleChange("patientSurname", e.value)}
                            />
                            <TextBox
                                value={call.patientName}
                                onValueChanged={(e) => handleChange("patientName", e.value)}
                            />
                            <TextBox
                                value={call.patientPatronymic}
                                onValueChanged={(e) => handleChange("patientPatronymic", e.value)}
                            />
                        </>
                    ) : (
                        [call.patientSurname, call.patientName, call.patientPatronymic]
                            .filter(Boolean).join(" ")
                    )}
                </div>
                <div><strong>Возраст:</strong>
                    {isModify ? (
                        <TextBox
                            value={call.patientAge}
                            onValueChanged={(e) => handleChange("patientAge", e.value)}
                        />
                    ) : (
                        call.patientAge
                    )}
                </div>
                <div><strong>Пол:</strong>
                    {isModify ? (
                        <SelectBox
                            value={call.patientSex}
                            onValueChanged={(e) => handleChange("patientSex", e.value)}
                            dataSource={genderOptions}
                            displayExpr="text"
                            valueExpr="value"
                        />
                    ) : (
                        getGender(call.patientSex)
                    )}
                </div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Локация</h2>
                <div><strong>Адрес:</strong>
                    {isModify ? (
                        <>
                            <TextBox
                                value={call.street}
                                onValueChanged={(e) => handleChange("street", e.value)}
                            />
                            <TextBox
                                value={call.streetNumber}
                                onValueChanged={(e) => handleChange("streetNumber", e.value)}
                            />
                            <TextBox
                                value={call.apartmentNumber}
                                onValueChanged={(e) => handleChange("apartmentNumber", e.value)}
                            />
                        </>
                    ) : (
                        [
                            `ул. ${call.street}`,
                            `д. ${call.streetNumber}`,
                            call.apartmentNumber && `кв. ${call.apartmentNumber}`
                        ].filter(Boolean).join(", ")
                    )}
                </div>
                <div><strong>Адрес госпитализации:</strong>
                    {isModify ? (
                        <TextBox
                            value={call.hospitalizationAddress}
                            onValueChanged={(e) => handleChange("hospitalizationAddress", e.value)}
                        />
                    ) : (
                        call.hospitalizationAddress
                    )}
                </div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Временные отметки</h2>
                {["receiveTime", "transferTime", "departureTime", "arrivalTime"].map((field) => (
                    <div key={field}>
                        <strong>{field}:</strong>
                        {isModify ? (
                            <DateBox
                                value={new Date(call[field])}
                                onValueChanged={(e) => handleChange(field, e.value.toISOString())}
                                type="datetime"
                            />
                        ) : (
                            formatDateTime(call[field])
                        )}
                    </div>
                ))}
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Детали бригады</h2>
                <div><strong>Бригада №:</strong>
                    {isModify ? (
                        <TextBox
                            value={call.brigadeNumber}
                            onValueChanged={(e) => handleChange("brigadeNumber", e.value)}
                        />
                    ) : (
                        call.brigadeNumber
                    )}
                </div>
                <div><strong>Номер ССМП:</strong>
                    {isModify ? (
                        <TextBox
                            value={call.ssmpNumber}
                            onValueChanged={(e) => handleChange("ssmpNumber", e.value)}
                        />
                    ) : (
                        call.ssmpNumber
                    )}
                </div>
                <div><strong>Причина вызова:</strong>
                    {isModify ? (
                        <TextBox
                            value={call.reason}
                            onValueChanged={(e) => handleChange("reason", e.value)}
                        />
                    ) : (
                        call.reason
                    )}
                </div>
            </div>
        </div>
    );
}
