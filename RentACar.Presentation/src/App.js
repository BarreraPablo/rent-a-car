// import logo from './logo.svg';
import "antd/dist/antd.css";
import {
    BrowserRouter as Router,
    Route
} from "react-router-dom";
import PrivateRoute from "./Components/PrivateRoute/PrivateRoute";
import { ProvideAuth } from "./Hooks/useAuth";
import Login from "./Pages/Login/Login";

function App() {
    return (
        <ProvideAuth>
            <Router>
                <PrivateRoute path="/home">
                    <h1>Test</h1>
                </PrivateRoute>
                <Route exact path={["/", "/login"]} component={Login} />
            </Router>
        </ProvideAuth>
    );
}

export default App;
