// import logo from './logo.svg';
import "antd/dist/antd.css";
import {
    BrowserRouter as Router,
    Route
} from "react-router-dom";
import MainLayout from "./Components/Layout/MainLayout";
import PrivateRoute from "./Components/PrivateRoute/PrivateRoute";
import { ProvideAuth } from "./Hooks/useAuth";
import { CarForm } from "./Pages/Cars/CarForm/CarForm";
import Cars from "./Pages/Cars/Cars";
import Login from "./Pages/Login/Login";

function App() {
    return (
        <ProvideAuth>
            <Router>
                <PrivateRoute path={["/cars", "/reservations"]}>
                    <MainLayout >
                        <Route exact path={['/cars/new', '/cars/edit/:id', '/cars/show/:id']} component={CarForm} />
                        <Route exact path='/cars' component={Cars} />
                        <Route path='/reservations' component={() => (<h1>Test2</h1>)} />
                    </MainLayout>
                </PrivateRoute>
                <Route exact path={["/", "/login"]} component={Login} />
            </Router>
        </ProvideAuth>
    );
}

export default App;
