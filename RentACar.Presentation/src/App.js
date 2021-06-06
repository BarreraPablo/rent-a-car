// import logo from './logo.svg';
import "antd/dist/antd.css";
import {
    HashRouter as Router,
    Route
} from "react-router-dom";
import MainLayout from "./Components/Layout/MainLayout";
import PrivateRoute from "./Components/PrivateRoute/PrivateRoute";
import { ProvideAuth } from "./Hooks/useAuth";
import { CarForm } from "./Pages/Cars/CarForm/CarForm";
import Cars from "./Pages/Cars/Cars";
import Login from "./Pages/Login/Login";
import Reservation from "./Pages/Reservation/Reservation"
import ReservationForm from "./Pages/Reservation/ReservationForm/ReservationForm"
import Client from "./Pages/Client/Client";
import ClientForm from "./Pages/Client/ClientForm/ClientForm";
import BodyType from "./Pages/BodyTypes/BodyType";
import Brand from "./Pages/Brands/Brand";
import PaymentType from "./Pages/PaymentTypes/PaymentType";
import DocumentType from "./Pages/DocumentTypes/DocumentType";
import RegisterForm from "./Pages/Register/Register";
import PasswordRecovery from "./Pages/PasswordRecovery/PasswordRecovery";
import PasswordReset from "./Pages/PasswordReset/PasswordReset";

function App() {
    return (
        <ProvideAuth>
            <Router basename={process.env.PUBLIC_URL}>
                <PrivateRoute path={["/cars", "/reservations", "/clients", "/bodyTypes", "/brands", "/documentypes", "/paymentypes"]}>
                    <MainLayout >
                        <Route exact path={['/cars/new', '/cars/edit/:id', '/cars/show/:id']} component={CarForm} />
                        <Route exact path='/cars' component={Cars} />
                        <Route exact path={['/reservations/new', '/reservations/show/:id', '/reservations/edit/:id']} component={ReservationForm} />
                        <Route exact path='/reservations' component={Reservation} />
                        <Route exact path={["/clients/new", "/clients/show/:id", "/clients/edit/:id"]} component={ClientForm} />
                        <Route exact path="/clients" component={Client} />
                        <Route exact path="/bodytypes" component={BodyType} />
                        <Route exact path="/brands" component={Brand} />
                        <Route exact path="/paymentypes" component={PaymentType} />
                        <Route exact path="/documentypes" component={DocumentType} />
                    </MainLayout>
                </PrivateRoute>
                <Route exact path="/register" component={RegisterForm} />
                <Route exact path="/passwordrecovery" component={PasswordRecovery} />
                <Route exact path="/passwordreset/:recoveryToken" component={PasswordReset} />
                <Route exact path={["/", "/login"]} component={Login} />
            </Router>
        </ProvideAuth>
    );
}

export default App;
