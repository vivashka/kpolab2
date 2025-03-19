import React, {useRef, useState} from "react";
import {Popup} from "devextreme-react/popup";
import {Form, Item, RequiredRule, DateBox} from "devextreme-react/form";
import {TabPanel, Item as TabItem} from "devextreme-react/tab-panel";
import {Button} from "devextreme-react/button";
import {getOrganizations} from "../../services/getOrganizations";
import store from "../../redux/store";
import {showModal} from "../../redux/reducers/error";
import {getCardiographs} from "../../services/getCardiographs";
import {SaveResult} from "../../services/SaveResult";
import {saveCardiogram} from "../../services/SaveCardiogram";
import {getCalls} from "../../services/getCalls";

export default function CardiogramCreation({isVisible, setIsVisible}) {
    const [organizations, setOrganizations] = useState([]);
    const [cardiographs, setCardiographs] = useState([]);
    const [calls, setCalls] = useState([]);

    // Состояние формы
    const [formData, setFormData] = useState({
        cardiogramUuid: "",
        receivedTime: new Date(),
        measurementTime: new Date(),
        cardiographUuid: "",
        callUuid: "",
        cardiogramState: 0,
        resultDescription: "",
        resultDiagnosisMain: "",
    });

    const hide = () => {
        setIsVisible(false);
    };
    const states = {
        0 : "Просмотрено",
        1 : "Не просмотрено",
        2 : "Утверждено",
    }
    async function onReadyShow() {
        const responseOrg = await getOrganizations();
        if (responseOrg.isSuccess) {
            setOrganizations(responseOrg.successEntity);
        } else {
            store.dispatch(showModal(responseOrg.errorEntity));
        }
        const responseCardiographs = await getCardiographs("");
        if (responseCardiographs.isSuccess) {
            setCardiographs(responseCardiographs.successEntity);
        } else {
            store.dispatch(showModal(responseCardiographs.errorEntity));
        }
        const responseCalls = await getCalls();
        if (responseCalls.isSuccess) {
            setCalls(responseCalls.successEntity);
        } else {
            store.dispatch(showModal(responseCalls.errorEntity));
        }
    }


    const onFieldDataChanged = (e) => {
        setFormData((prev) => ({
            ...prev, [e.dataField]: e.value,
        }));
    };

    // Обработчик отправки формы
    const handleSubmit = async () => {
        const requestModelResult = {
            resultCardiogramUuid: "",
            description: formData.resultDescription,
            diagnosisMain: formData.resultDiagnosisMain,
        }
        const cardiogram = {
            cardiogramUuid: formData.cardiogramUuid,
            receivedTime: formData.receivedTime,
            measurementTime: formData.measurementTime,
            cardiographUuid: formData.cardiographUuid,
            callUuid: formData.callUuid,
            cardiogramState: formData.cardiogramState,
            resultCardiogramUuid: "",
        };
        try{
            const responseResult = await SaveResult(requestModelResult)
            if (responseResult.isSuccess) {
                cardiogram.resultCardiogramUuid = responseResult.successEntity.resultCardiogramUuid
            }
            else {
                store.dispatch(showModal(responseResult.errorEntity));
            }

            const responseCardiogram = await saveCardiogram(cardiogram);
            console.log(cardiogram)
            if (responseCardiogram.isSuccess){
                console.log("Успешное создание")
                hide()
            }
            else {
                store.dispatch(showModal(responseCardiogram.errorEntity));
            }
        } catch (e){
            store.dispatch(showModal({errorCode: "400", errorMessage:"Непредвиденная ошибка"}))
        }


    };

    return (
        <Popup
            visible={isVisible}
            onHiding={hide}
            hideOnOutsideClick={true}
            title="Создание кардиограммы"
            onShowing={onReadyShow}
        >

            <Form formData={formData} onFieldDataChanged={onFieldDataChanged}>
                <Item
                    dataField="receivedTime"
                    editorType="dxDateBox"
                    label={{ text: "Дата получения" }}
                    editorOptions={{
                        type: "datetime",
                        displayFormat: "yyyy-MM-dd HH:mm:ss",
                    }}
                >
                    <RequiredRule message="Дата получения обязательна" />
                </Item>

                <Item
                    dataField="measurementTime"
                    editorType="dxDateBox"
                    label={{ text: "Дата измерения" }}
                    editorOptions={{
                        type: "datetime",
                        displayFormat: "yyyy-MM-dd HH:mm:ss",
                    }}
                >
                    <RequiredRule message="Дата измерения обязательна" />
                </Item>

                <Item
                    dataField="cardiographUuid"
                    editorType="dxSelectBox"
                    label={{ text: "Кардиограф" }}
                    editorOptions={{
                        dataSource: cardiographs,
                        displayExpr: (item) => item ? `${item.manufacturerName} (${item.serialNumber})` : "",
                        valueExpr: "serialNumber",
                    }}
                >
                    <RequiredRule message="Необходимо выбрать кардиограф" />
                </Item>

                <Item
                    dataField="organizationUuid"
                    editorType="dxSelectBox"
                    label={{ text: "Организация" }}
                    editorOptions={{
                        dataSource: organizations,
                        displayExpr: "name",
                        valueExpr: "organizationUuid",
                    }}
                >
                    <RequiredRule message="Необходимо выбрать организацию" />
                </Item>

                <Item
                    dataField="cardiogramState"
                    editorType="dxSelectBox"
                    label={{ text: "Статус кардиограммы" }}
                    editorOptions={{
                            dataSource: Object.values(states)
                        }}
                >
                    <RequiredRule message="Введите статус кардиограммы" />
                </Item>

                <Item
                    dataField="resultDiagnosisMain"
                    editorType="dxTextBox"
                    label={{ text: "Диагноз" }}
                >
                    <RequiredRule message="Введите диагноз" />
                </Item>

                <Item
                    dataField="resultDescription"
                    editorType="dxTextArea"
                    label={{ text: "Описание диагноза" }}
                >
                    <RequiredRule message="Введите описание диагноза" />
                </Item>
            </Form>

            <Button text="Создать" type="success" onClick={handleSubmit}/>
        </Popup>
    );
}
