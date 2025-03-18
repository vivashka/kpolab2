import { formatDate } from "devextreme/localization";
import { TextBox, SelectBox, DateBox } from "devextreme-react";
import {useEffect, useState} from "react";
import {saveCardiogram} from "../services/SaveCardiogram";

export function CardiogramView({ cardiogram, isModify }) {

    const cardiogramState = {
        cardiogramUuid: cardiogram.cardiogramUuid,
        receivedTime: cardiogram.receivedTime,
        measurementTime: cardiogram.measurementTime,
        rawCardiogram: cardiogram.rawCardiogram,
        cardiogramState: cardiogram.cardiogramState,
        callUuid: cardiogram.call.callUuid,
        cardiographUuid: cardiogram.cardiograph.serialNumber,
        resultCardiogramUuid: cardiogram.result.resultCardiogramUuid
    }

    const [newData, setNewData] = useState(cardiogramState || {});

    const formatDateTime = (isoString) => {
        const date = new Date(isoString);
        return formatDate(date, "dd.MM.yyyy HH:mm") || "Не указано";
    };

    const handleChange = (field, value) => {
        setNewData(prev => ({ ...prev, [field]: value }));
    };

    useEffect(() => {

        if (!isModify) {
            async function pushData() {
                console.log(newData)
                const response = await saveCardiogram(newData);
                if (response.cardiogramUuid) {
                    console.log("Успешно обновлено");
                    setNewData(response)
                }
            }
            pushData();
        }
    }, [isModify]);

    // Пример возможных статусов для SelectBox
    const states = {
        0 : "Просмотрено",
        1 : "Не просмотрено",
        2 : "Утверждено",
    }
    function getKeyByValue(object, value) {
        return Object.keys(object).find(key => object[key] === value);
    }

    return (
        <div className="cardiogram-view">
            <h1>Кардиограмма</h1>

            <div className="field">
                <label>Статус:</label>
                {isModify ? (
                    <SelectBox
                        value={states[newData.cardiogramState]}
                        onValueChanged={(e) => {
                            handleChange("cardiogramState", Number(getKeyByValue(states, e.value)));
                            console.log(e)
                        }}
                        dataSource={Object.values(states)}
                    />
                ) : (
                    <span>{states[newData.cardiogramState]}</span>
                )}
            </div>

            <div className="field">
                <label>Время измерения:</label>
                {isModify ? (
                    <DateBox
                        value={new Date(newData.measurementTime)}
                        onValueChanged={(e) =>
                            handleChange("measurementTime", e.value.toISOString())
                        }
                        type="datetime"
                    />
                ) : (
                    <span>{formatDateTime(newData.measurementTime)}</span>
                )}
            </div>

            <div className="field">
                <label>Время получения:</label>
                {isModify ? (
                    <DateBox
                        value={new Date(newData.receivedTime)}
                        onValueChanged={(e) =>
                            handleChange("measurementTime", e.value.toISOString())
                        }
                        type="datetime"
                    />
                ) : (
                    <span>{formatDateTime(newData.receivedTime)}</span>
                )}
            </div>
        </div>
    );
}