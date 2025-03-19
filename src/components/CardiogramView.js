import { formatDate } from "devextreme/localization";
import {TextBox, SelectBox, DateBox, Form} from "devextreme-react";
import {useEffect, useState} from "react";
import {saveCardiogram} from "../services/SaveCardiogram";
import {CustomRule} from "devextreme-react/form";
import {showModal} from "../redux/reducers/error";

export function CardiogramView({ cardiogram, isModify, dispatch, isSave }) {

    const getCardiogramState = (c) => ({
        cardiogramUuid: c.cardiogramUuid,
        receivedTime: c.receivedTime,
        measurementTime: c.measurementTime,
        rawCardiogram: c.rawCardiogram,
        cardiogramState: c.cardiogramState,
        callUuid: c.call.callUuid,
        cardiographUuid: c.cardiograph.serialNumber,
        resultCardiogramUuid: c.result.resultCardiogramUuid
    });

    // Инициализация state на основе cardiogram из props
    const [newData, setNewData] = useState(getCardiogramState(cardiogram));

    const formatDateTime = (isoString) => {
        const date = new Date(isoString);
        return formatDate(date, "dd.MM.yyyy HH:mm") || "Не указано";
    };
    useEffect(() => {
        setNewData(getCardiogramState(cardiogram));
    }, [cardiogram]);

    const handleChange = (field, value) => {
        setNewData(prev => ({ ...prev, [field]: value }));
    };

    useEffect(() => {

        if (!isModify && isSave) {
            async function pushData() {
                console.log(newData)
                const response = await saveCardiogram(newData);
                if (response.isSuccess) {
                    console.log("Успешно обновлено");
                    setNewData(response.successEntity)
                }
                else {
                    dispatch(showModal(response.errorEntity))
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

    const validateDates = () => {
        return newData.measurementTime > newData.receivedTime;
    };

    return (
        <div className="cardiogram-view" >
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
                            handleChange("receivedTime", e.value.toISOString())
                        }
                        type="datetime"
                    >
                    </DateBox>
                ) : (
                    <span>{formatDateTime(newData.receivedTime)}</span>
                )}
            </div>
        </div>
    );
}