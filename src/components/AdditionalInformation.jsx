import {Popup, TabPanel} from "devextreme-react";
import {CardiogramView} from "./CardiogramView";
import {Item} from "devextreme-react/tab-panel";
import CallView from "./CallView";
import {ResultView} from "./ResultView";
import {CardiographView} from "./CardiographView";


export default function AdditionalInformation({
                                                  data,
                                                  visible,
                                              setVisible}) {

    const hide = () => {
        setVisible(false)
    };



    return (
        <Popup visible={visible} onHiding={hide}>
                <TabPanel
                    swipeEnabled={true}
                    animationEnabled={true}
                    tabsPosition={"top"}
                    stylingMode={"secondary"}
                >

                    <Item title="Кардиограмма" >
                        <CardiogramView cardiogram={data} />
                    </Item>
                    <Item title="Вызов" >
                        <CallView call={data.call} />
                    </Item>
                    <Item title="Результат" >
                        <ResultView result={data.result} />
                    </Item>
                    <Item title="Кардиограф" >
                        <CardiographView cardiograph={data.cardiograph} />
                    </Item>

                </TabPanel>
        </Popup>

    )

}