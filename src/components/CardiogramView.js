import { formatDate } from "devextreme/localization";

export function CardiogramView({cardiogram}) {
    const formatDateTime = (isoString) => {
        const date = new Date(isoString);
        return formatDate(date, "dd.MM.yyyy HH:mm") || "Не указано";
    };

    return(
        <div>
            <h1>Кардиограмма</h1>
            <div>
                Статус: {cardiogram.cardiogramState}
            </div>
            <div>
                Время измерения: {formatDateTime(cardiogram.measurementTime)}
            </div>
            <div>
                Вызов: {cardiogram.call.dayNumber}/{cardiogram.call.yearNumber}
            </div>
            <div>
                Результат {cardiogram.result.description}
            </div>
            <div>
                Кардиограф {cardiogram.cardiograph.serialNumber}
            </div>
        </div>
    )
}