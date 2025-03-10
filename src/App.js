import ruMessages from "devextreme/localization/messages/ru.json";
import React from 'react';
import {loadMessages, locale} from "devextreme/localization";
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import {MainPage} from "./pages/main-page";
import './devextreme-theme/dx.generic.arm-theme.css'

const routerProvider = createBrowserRouter([
    {
        path: "/main-page",
        element: <MainPage />,
    },
    {
        path: "*",
        element: <MainPage />,
    }
])

const App = () => {
    loadMessages(ruMessages);
    locale(navigator.language);

    return (
        <RouterProvider router={routerProvider}/>
    );
};

export default App;