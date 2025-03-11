import {Button, Popup, TabPanel} from "devextreme-react";
import {CardiogramView} from "./CardiogramView";
import {Item} from "devextreme-react/tab-panel";
import CallView from "./CallView";
import {ResultView} from "./ResultView";
import {CardiographView} from "./CardiographView";
import {useState} from "react";


export default function AdditionalInformation({
                                                  data,
                                                  visible,
                                              setVisible}) {
    const [isModify, setIsModify] = useState(false);


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
                    animationEnabled={true}
                    tabsPosition={"top"}
                    stylingMode={"secondary"}

                >

                    <Item title="Кардиограмма" >
                        <CardiogramView
                            cardiogram={data}
                            isModify={isModify}
                            onFieldChange={(updated) => setSelectedCardiogram(updated)}
                        />
                    </Item>
                    <Item title="Вызов" >
                        <CallView call={data.call} isModify={isModify} onFieldChange={(updated) => setSelectedCall(updated)} />
                    </Item>
                    <Item title="Результат" >
                        <ResultView result={data.result} isModify={isModify} onFieldChange={(updated) => setSelectedResult(updated)} />
                    </Item>
                    <Item title="Кардиограф" >
                        <CardiographView cardiograph={data.cardiograph} isModify={isModify} onFieldChange={(updated) => setSelectedCardiograph(updated)} />
                    </Item>

                </TabPanel>
            <div className="additional-buttons">
                <Button className={"button button-change"} text={"Изменить"} onClick={() => setIsModify(true)} />
                <Button className={"button button-save"} text={"Сохранить"} onClick={() => setIsModify(false)} />
            </div>


        </Popup>

    )

}