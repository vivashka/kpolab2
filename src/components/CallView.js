import { formatDate } from "devextreme/localization";
import { TextBox, SelectBox, DateBox } from "devextreme-react";
import {useEffect, useState} from "react";
import {SaveCall} from "../services/SaveCall";
import {showModal} from "../redux/reducers/error";

export default function CallView({ call, isModify, isSave, dispatch }) {

    const [newData, setNewData] = useState(call || {}); // Инициализируем данными

    const handleChange = (field, value) => {
        setNewData(prev => ({ ...prev, [field]: value })); // Меняем только одно поле
    };
    useEffect(() => {
        setNewData(call || {});
    }, [call]);

    useEffect(() => {

        if (!isModify && isSave) {
            async function pushData() {
                console.log(newData)
                const response = await SaveCall(newData);
                if (response.isSuccess) {
                    console.log("Успешно обновлено");
                    setNewData(response.successEntity)
                }
                else {
                    dispatch(showModal(response?.errorEntity))
                }
            }
            pushData();
        }
    }, [isModify]);

    const formatDateTime = (isoString) => {
        const date = new Date(isoString);
        return formatDate(date, "dd.MM.yyyy HH:mm") || "Не указано";
    };

    if (!call) return <div>Нет данных о вызове</div>;
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
                            value={newData.callType}
                            onValueChanged={(e) => handleChange("callType", e.value)}
                        />
                    ) : (
                        newData.callType || "Не указан"
                    )}
                </div>
                <div><strong>Приоритет:</strong>
                    {isModify ? (
                        <TextBox
                            value={Number(newData.priority)}
                            onValueChanged={(e) => handleChange("priority", Number(e.value))}
                        />
                    ) : (
                        Number(newData.priority)
                    )}
                </div>
                <div><strong>Диагноз:</strong>
                    {isModify ? (
                        <TextBox mode={""}
                            value={newData.callDiagnosis}
                            onValueChanged={(e) => handleChange("callDiagnosis", e.value)}
                        />
                    ) : (
                        newData.callDiagnosis || "Не указан"
                    )}
                </div>
                <div><strong>Номер:</strong>
                    {isModify ? (
                        <div className="call-input">
                            <TextBox
                                value={call?.dayNumber}
                                onValueChanged={(e) =>
                                    handleChange("call", {
                                        ...cardiogram.call,
                                        dayNumber: e.value
                                    })
                                }
                            />
                            /
                            <TextBox
                                value={call?.yearNumber}
                                onValueChanged={(e) =>
                                    handleChange("call", {
                                        ...cardiogram.call,
                                        yearNumber: e.value
                                    })
                                }
                            />
                        </div>
                    ) : (
                        <span>
            {call?.dayNumber}/{call?.yearNumber}
          </span>
                    )}
                </div>
            </div>

            <div className="section" style={{marginTop: "15px"}}>
                <h2>Пациент</h2>
                <div><strong>ФИО:</strong>
                    {isModify ? (
                        <>
                            <TextBox
                                value={newData.patientSurname}
                                onValueChanged={(e) => handleChange("patientSurname", e.value)}
                            />
                            <TextBox
                                value={newData.patientName}
                                onValueChanged={(e) => handleChange("patientName", e.value)}
                            />
                            <TextBox
                                value={newData.patientPatronymic}
                                onValueChanged={(e) => handleChange("patientPatronymic", e.value)}
                            />
                        </>
                    ) : (
                        [newData.patientSurname, newData.patientName, newData.patientPatronymic]
                            .filter(Boolean).join(" ")
                    )}
                </div>
                <div><strong>Возраст:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.patientAge}
                            onValueChanged={(e) => handleChange("patientAge", e.value)}
                        />
                    ) : (
                        newData.patientAge
                    )}
                </div>
                <div><strong>Пол:</strong>
                    {isModify ? (
                        <SelectBox
                            value={newData.patientSex}
                            onValueChanged={(e) => handleChange("patientSex", e.value)}
                            dataSource={genderOptions}
                            displayExpr="text"
                            valueExpr="value"
                        />
                    ) : (
                        getGender(newData.patientSex)
                    )}
                </div>
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Локация</h2>
                <div><strong>Адрес:</strong>
                    {isModify ? (
                        <>
                            <TextBox
                                value={newData.street}
                                onValueChanged={(e) => handleChange("street", e.value)}
                            />
                            <TextBox
                                value={newData.streetNumber}
                                onValueChanged={(e) => handleChange("streetNumber", e.value)}
                            />
                            <TextBox
                                value={newData.apartmentNumber}
                                onValueChanged={(e) => handleChange("apartmentNumber", e.value)}
                            />
                        </>
                    ) : (
                        [
                            `ул. ${newData.street}`,
                            `д. ${newData.streetNumber}`,
                            newData.apartmentNumber && `кв. ${newData.apartmentNumber}`
                        ].filter(Boolean).join(", ")
                    )}
                </div>
                <div><strong>Адрес госпитализации:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.hospitalizationAddress}
                            onValueChanged={(e) => handleChange("hospitalizationAddress", e.value)}
                        />
                    ) : (
                        newData.hospitalizationAddress
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
                                value={new Date(newData[field])}
                                onValueChanged={(e) => handleChange(field, e.value.toISOString())}
                                type="datetime"
                            />
                        ) : (
                            formatDateTime(newData[field])
                        )}
                    </div>
                ))}
            </div>

            <div className="section" style={{ marginTop: "15px" }}>
                <h2>Детали бригады</h2>
                <div><strong>Бригада №:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.brigadeNumber}
                            onValueChanged={(e) => handleChange("brigadeNumber", e.value)}
                        />
                    ) : (
                        newData.brigadeNumber
                    )}
                </div>
                <div><strong>Номер ССМП:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.ssmpNumber}
                            onValueChanged={(e) => handleChange("ssmpNumber", e.value)}
                        />
                    ) : (
                        newData.ssmpNumber
                    )}
                </div>
                <div><strong>Причина вызова:</strong>
                    {isModify ? (
                        <TextBox
                            value={newData.reason}
                            onValueChanged={(e) => handleChange("reason", e.value)}
                        />
                    ) : (
                        newData.reason
                    )}
                </div>
            </div>
        </div>
    );
}
