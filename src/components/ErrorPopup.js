import React from 'react';
import {Popup} from 'devextreme-react/popup';
import {useSelector, useDispatch} from 'react-redux';
import {closeModal} from "../redux/reducers/error";

export function ErrorPopup() {

    const modal = useSelector(state => state.errorModal);
    const dispatch = useDispatch();
    const hide = () => {
        dispatch(closeModal());
    };

    return (
        <Popup accessKey={"Error"} title="Ошибка" width={'30%'} height={'30%'} visible={modal.visible} onHiding={hide}
               hideOnOutsideClick={true}>
            <div key={"code"}>
                Код: {modal.errorCode}
            </div>
            <div key={"message"}>
                Сообщение: {modal?.errorMessage ?? "Непредвиденная ошибка"}
            </div>
        </Popup>
    )
}

export default ErrorPopup