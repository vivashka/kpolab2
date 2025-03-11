import { formatDate } from "devextreme/localization";
import { TextBox, SelectBox, DateBox } from "devextreme-react";

export function CardiogramView({ cardiogram, isModify, onFieldChange }) {
    const formatDateTime = (isoString) => {
        const date = new Date(isoString);
        return formatDate(date, "dd.MM.yyyy HH:mm") || "Не указано";
    };

    const handleChange = (field, value) => {
        if (onFieldChange) {
            onFieldChange({ ...cardiogram, [field]: value });
        }
    };

    // Пример возможных статусов для SelectBox
    const states = {
        0 : "Просмотрено",
        1 : "Не просмотрено",
        2 : "Утверждено",

    }

    return (
        <div className="cardiogram-view">
            <h1>Кардиограмма</h1>

            <div className="field">
                <label>Статус:</label>
                {isModify ? (
                    <SelectBox
                        value={states[cardiogram.cardiogramState]}
                        onValueChanged={(e) => handleChange("cardiogramState", e.value)}
                        dataSource={Object.values(states)}
                    />
                ) : (
                    <span>{states[cardiogram.cardiogramState]}</span>
                )}
            </div>

            <div className="field">
                <label>Время измерения:</label>
                {isModify ? (
                    <DateBox
                        value={new Date(cardiogram.measurementTime)}
                        onValueChanged={(e) =>
                            handleChange("measurementTime", e.value.toISOString())
                        }
                        type="datetime"
                    />
                ) : (
                    <span>{formatDateTime(cardiogram.measurementTime)}</span>
                )}
            </div>

            <div className="field">
                <label>Вызов:</label>
                {isModify ? (
                    <div className="call-input">
                        <TextBox
                            value={cardiogram.call?.dayNumber}
                            onValueChanged={(e) =>
                                handleChange("call", {
                                    ...cardiogram.call,
                                    dayNumber: e.value
                                })
                            }
                        />
                        /
                        <TextBox
                            value={cardiogram.call?.yearNumber}
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
            {cardiogram.call?.dayNumber}/{cardiogram.call?.yearNumber}
          </span>
                )}
            </div>

            <div className="field">
                <label>Результат:</label>
                {isModify ? (
                    <TextBox
                        value={cardiogram.result?.description}
                        onValueChanged={(e) =>
                            handleChange("result", {
                                ...cardiogram.result,
                                description: e.value
                            })
                        }
                    />
                ) : (
                    <span>{cardiogram.result?.description}</span>
                )}
            </div>

            <div className="field">
                <label>Кардиограф:</label>
                {isModify ? (
                    <TextBox
                        value={cardiogram.cardiograph?.serialNumber}
                        onValueChanged={(e) =>
                            handleChange("cardiograph", {
                                ...cardiogram.cardiograph,
                                serialNumber: e.value
                            })
                        }
                    />
                ) : (
                    <span>{cardiogram.cardiograph?.serialNumber}</span>
                )}
            </div>
        </div>
    );
}