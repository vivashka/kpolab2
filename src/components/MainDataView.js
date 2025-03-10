import React, {useEffect, useState} from "react";
import {getListCardiograms} from "../services/getListCardiograms";
import TreeView from 'devextreme-react/tree-view';
import {Popup} from "devextreme-react";
import AdditionalInformation from "./AdditionalInformation.jsx";
import {tabKindEnum} from "../domain/additionalInformationSource";
import {getEntireCardiogram} from "../services/getEntireCardiogram";
import {formatDate} from "devextreme/localization";

export default function MainDataView() {
    const [data, setData] = useState(null);
    const [currentItem, setCurrentItem] = useState(null);
    const [itemVisible, setItemVisible] = useState(null);

    const defaultFilter = {
        "dateFrom": "2023-03-10T09:10:23.121Z",
        "dateTo": "2025-03-10T09:10:23.121Z",
        "sortAttribute": 0,
        "sortMode": 0
    }



    useEffect(() =>{
        async function fetchData(){
            try {
                const request = await getListCardiograms(defaultFilter);
                if (request.success) {
                    const transformedData = request.content.map(cardiogram => ({
                        id: cardiogram.cardiogramUuid,
                        text: `Кардиограмма: ${formatDateTime(cardiogram.receivedTime)}`,
                        items: [
                            {
                                id: `${cardiogram.cardiogramUuid}-call`,
                                text: `Пациент: ${cardiogram.call.patientSurname} ${cardiogram.call.patientName}`,
                                data: cardiogram.call,
                                items:[
                                    {
                                        id: `${cardiogram.cardiogramUuid}-result`,
                                        text: `Результат: ${cardiogram.result.diagnosisMain}`,
                                        data: cardiogram.result,
                                        isLeaf: true
                                    }

                                ],
                            },
                        ]
                    }));
                    setData(transformedData);
                }
            } catch (error) {
                console.error("Ошибка при загрузке данных:", error);
            }
        }
        fetchData();
    },[])

    async function selectItem(e) {

        if (e?.itemData?.id?.includes("-result")) {
            const request = await getEntireCardiogram(e.itemData.id.replace("-result", ""));

            if (request.cardiogramUuid != null) {
                setCurrentItem(request);
            }
            setItemVisible(true);
        }
    }

    const formatDateTime = (isoString) => {
        const date = new Date(isoString);
        return formatDate(date, "dd.MM.yyyy HH:mm") || "Не указано";
    };


    return (<div className="form">
            {currentItem && <AdditionalInformation data={currentItem} visible={itemVisible} setVisible={setItemVisible}/>}
            <TreeView
                items={data}
                onItemClick={selectItem}
            >

            </TreeView>
        </div>

    )
}
