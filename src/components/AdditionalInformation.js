import {Button, Popup, TabPanel} from "devextreme-react";
import {CardiogramView} from "./CardiogramView";
import {Item} from "devextreme-react/tab-panel";
import CallView from "./CallView";
import {ResultView} from "./ResultView";
import {CardiographView} from "./CardiographView";
import {useEffect, useState} from "react";
import {UserView} from "./UserView";
import {UsersList} from "./UsersList";
import {getCardiograms} from "../services/getUsersByCardiograms";


export default function AdditionalInformation({
                                                  data,
                                                  visible,
                                                  setVisible
                                              }) {
    const [isModify, setIsModify] = useState(false);
    const [isSave, setIsSave] = useState(false);

    const [users, setUsers] = useState([])

    useEffect(() => {
        async function fetchData() {
            const request = await getCardiograms(data.cardiogramUuid);
            console.log(request);
            if (request.length > 0) {
                setUsers(request)
            }
        }

        fetchData()
    }, []);
    const hide = () => {
        setVisible(false)
    };

    const handleUsersChange = (updatedUsers) => {
        setUsers(updatedUsers);
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
                        isModify={isModify}
                    />
                </Item>
                <Item title="Вызов">
                    <CallView call={data.call} isModify={isModify} isSave={isSave} />
                </Item>
                <Item title="Результат">
                    <ResultView result={data.result} isModify={isModify}/>
                </Item>
                <Item title="Кардиограф">
                    <CardiographView cardiograph={data.cardiograph} isModify={isModify}/>
                </Item>
                <Item title="Авторы">
                    <UsersList users={users} isModify={isModify}/>
                </Item>

            </TabPanel>
            <div className="additional-buttons">
                <Button className={"button button-change"} text={"Изменить"} onClick={() => setIsModify(true)}/>
                <Button
                    className={"button button-apply"}
                    text={"Сохранить"}
                    onClick={() => {
                        setIsModify(false)
                        setIsSave(true)}
                    }
                    disabled={!isModify}/>
            </div>


        </Popup>

    )

}