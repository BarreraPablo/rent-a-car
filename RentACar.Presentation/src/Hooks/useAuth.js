import React, { useState, useContext, createContext, useEffect } from "react";
import axios from "axios";

const api_url = process.env.REACT_APP_API_URL;

const authContext = createContext();

export function ProvideAuth({ children }) {
    const auth = useProvideAuth();
    return <authContext.Provider value={auth}>{children}</authContext.Provider>;
}

export const useAuth = () => {
    return useContext(authContext);
};

function useProvideAuth() {
    const tryGetUser = () => {
        const token = localStorage.getItem("token");
        const userName = localStorage.getItem("username");
        if (token) {
            axios.defaults.headers.common["Authorization"] = "Bearer " + token;
            return { username: userName };
        } else {
            return false;
        }
    };

    const [user, setUser] = useState(tryGetUser);

    const signin = (username, password, remember) => {
        const bodyRequest = {
            Username: username,
            password: password,
        };

        return axios
            .post(api_url + "token", bodyRequest)
            .then((res) => {
                if (remember) {
                    localStorage.setItem("token", res.data.token);
                    localStorage.setItem("username", username);
                }
                axios.defaults.headers.common["Authorization"] = "Bearer " + res.data.token;

                setUser({ username: username });
                return Promise.resolve(res);
            })
            .catch((err) => {
                if (err.response && err.response.status === 401) {
                    return Promise.reject(
                        new Error("Incorrect username or password")
                    );
                }
                return Promise.reject(
                    new Error("An unexpected error occurred")
                );
            });
    };

    const signout = () => {
        setUser(false);
        localStorage.removeItem("token");
    };

    return {
        user,
        signin,
        signout
    };
}