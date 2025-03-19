import React, { useState, useRef } from 'react';
import { Popup } from 'devextreme-react/popup';
import {
    Form,
    Item,
    RequiredRule,
    StringLengthRule,
    PatternRule
} from 'devextreme-react/form';
import CryptoJS from 'crypto-js';
import { getOrganizations } from "../services/getOrganizations";
import store from "../redux/store";
import { showModal } from "../redux/reducers/error";
import {registration} from "../services/registration";
import {login_success} from "../redux/reducers/user";

export function RegistrationUser() {
    const [isVisible, setIsVisible] = useState(true);
    const [formData, setFormData] = useState({
        login: '',
        password: '',
        phoneNumber: '',
        fullName: '',
        organizationUuid: '',
        appointment: ''
    });
    const [organizations, setOrganizations] = useState([]);

    // Реф для формы, чтобы вызвать метод validate()
    const formRef = useRef(null);

    const hide = () => {
        setIsVisible(false);
    };

    const onFieldDataChanged = (e) => {
        setFormData(prev => ({ ...prev, [e.dataField]: e.value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        // Валидируем форму через ref
        const validationResult = formRef.current.instance.validate();
        console.log('formData:', formData);
        console.log('validationResult:', validationResult);
        if (!validationResult.isValid) {
            return; // Если валидация не пройдена – ничего не делаем
        }
        console.log("Данные для регистрации:", formData);

        const response = await registration(formData)
        if (response.isSuccess){
            store.dispatch(login_success(response.successEntity))
            hide();
        }
        else {
            store.dispatch(showModal(response.errorEntity))
        }


    };

    async function onReadyShow() {
        const response = await getOrganizations();
        if (response.isSuccess) {
            setOrganizations(response.successEntity);
        } else {
            store.dispatch(showModal(response.errorEntity));
        }
    }

    return (
        <Popup
            title="Регистрация пользователя"
            visible={isVisible}
            onHiding={hide}
            hideOnOutsideClick={true}
            width={600}
            height={500}
            onShowing={onReadyShow}
        >
            <form onSubmit={handleSubmit}>
                <Form
                    formData={formData}
                    onFieldDataChanged={onFieldDataChanged}
                    ref={formRef}
                >
                    <Item dataField="login" editorType="dxTextBox">
                        <RequiredRule message="Логин обязателен" />
                        <StringLengthRule min={4} message="Логин должен содержать минимум 4 символа" />
                    </Item>
                    <Item
                        dataField="password"
                        editorType="dxTextBox"
                        editorOptions={{ mode: 'password' }}
                    >
                        <RequiredRule message="Пароль обязателен" />
                        <StringLengthRule min={4} message="Пароль должен содержать минимум 4 символов" />
                    </Item>
                    <Item dataField="phoneNumber" editorType="dxTextBox">
                        <RequiredRule message="Номер телефона обязателен" />
                        <PatternRule pattern="^\d{10,15}$" message="Введите корректный номер телефона (10-15 цифр)" />
                    </Item>
                    <Item dataField="fullName" editorType="dxTextBox">
                        <RequiredRule message="ФИО обязательно" />
                    </Item>
                    <Item
                        dataField="organizationUuid"
                        editorType="dxSelectBox"
                        editorOptions={{
                            dataSource: organizations,
                            displayExpr: 'name',
                            valueExpr: 'organizationUuid'
                        }}
                    >
                        <RequiredRule message="Необходимо выбрать организацию" />
                    </Item>
                    <Item dataField="appointment" editorType="dxTextBox">
                        <RequiredRule message="Поле должность обязательно" />
                    </Item>
                    {/* Кнопка отправки. Используем useSubmitBehavior, чтобы форма вызывала onSubmit */}
                    <Item
                        itemType="button"
                        horizontalAlignment="right"
                        buttonOptions={{
                            text: 'Зарегистрироваться',
                            type: 'success',
                            useSubmitBehavior: true
                        }}
                    />
                </Form>
            </form>
        </Popup>
    );
}
