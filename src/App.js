import ruMessages from "devextreme/localization/messages/ru.json";
import React from 'react';
import {loadMessages, locale} from "devextreme/localization";
import TreeList, {
    Column,
    ColumnChooser,
    HeaderFilter,
    SearchPanel,
    Selection,
    Lookup,
} from 'devextreme-react/tree-list';

const App = () => {
    loadMessages(ruMessages);
    locale(navigator.language);

    return (
        <div>
            <TreeList></TreeList>
        </div>
    );
};

export default App;