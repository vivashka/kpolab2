import {createRoot} from "react-dom/client";
import App from "./App";
import ErrorPopup from "./components/ErrorPopup";
import {Provider} from "react-redux";
import {store} from './redux/store';

const root = createRoot(document.getElementById('root'));

root.render(
    <Provider store={store}>
        <App/>
        <ErrorPopup/>
    </Provider>
);