// import logo from './logo.svg';
import "antd/dist/antd.css";
import {
    BrowserRouter as Router,
    Route
} from "react-router-dom";
import PrivateRoute from "./Components/PrivateRoute/PrivateRoute";
import { ProvideAuth } from "./Hooks/useAuth";
import Login from "./Pages/Login/Login";
import MainLayout from "./Components/Layout/MainLayout"
import { Children } from "react";

function App() {
    return (
        <ProvideAuth>
            <Router>
                <PrivateRoute path={["/cars", "/reservations"]}>
                    <MainLayout >
                        <Route path='/cars' component={() => (<h1>Test1</h1>)} />
                        <Route path='/reservations' component={() => (<h1>Test2</h1>)} />
                    </MainLayout>
                </PrivateRoute>
                <Route exact path={["/", "/login"]} component={Login} />
            </Router>
        </ProvideAuth>
    );
}

export default App;
