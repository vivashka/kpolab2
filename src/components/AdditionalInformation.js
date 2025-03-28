import {Button, Popup, TabPanel} from "devextreme-react";
import {CardiogramView} from "./CardiogramView";
import {Item} from "devextreme-react/tab-panel";
import CallView from "./CallView";
import {ResultView} from "./ResultView";
import {CardiographView} from "./CardiographView";
import {useEffect, useState} from "react";
import {UserView} from "./UserView";
import {UsersList} from "./UsersList";
import {getUsersByCardiograms} from "../services/getUsersByCardiograms";
import {useDispatch} from "react-redux";


export default function AdditionalInformation({
                                                  data,
                                                  visible,
                                                  setVisible,
                                                  modify, setModify
                                              }) {
    const [isSave, setIsSave] = useState(false);

    const dispatch = useDispatch();

    const [users, setUsers] = useState([])
    useEffect(() => {
        async function fetchData() {
            const request = await getUsersByCardiograms(data.cardiogramUuid);
            if (request.length > 0) {
                setUsers(request)
            }
        }

        fetchData()
    }, []);
    const hide = () => {
        setVisible(false)
    };


    return (
        <Popup
            visible={visible}
            onHiding={hide}
            hideOnOutsideClick={true}
            title="Доплонительная информация"
        >
            <TabPanel
                className={"tab-info"}
                animationEnabled={true}
                tabsPosition={"top"}
                stylingMode={"secondary"}

            >

                <Item title="Кардиограмма">
                    <CardiogramView
                        cardiogram={data}
                        isModify={modify}
                        isSave={isSave}
                        dispatch={dispatch}
                    />
                </Item>
                <Item title="Вызов">
                    <CallView call={data.call} isModify={modify} isSave={isSave} dispatch={dispatch}/>
                </Item>
                <Item title="Результат">
                    <ResultView result={data.result} isModify={modify} isSave={isSave} dispatch={dispatch}/>
                </Item>
                <Item title="Кардиограф">
                    <CardiographView cardiograph={data.cardiograph} isModify={modify} isSave={isSave}
                                     dispatch={dispatch}/>
                </Item>
                <Item title="Авторы">
                    <UsersList users={users} isModify={modify} isSave={isSave} dispatch={dispatch}/>
                </Item>

            </TabPanel>
            <div className="additional-buttons">
                <Button className={"button button-change"} text={"Изменить"} onClick={() => setModify(true)}/>
                <Button
                    className={"button button-apply"}
                    text={"Сохранить"}
                    onClick={() => {
                        setModify(false)
                        setIsSave(true)
                    }
                    }
                    disabled={!modify}/>
            </div>


        </Popup>

    )

}