import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import axios from 'axios';

const api_url = process.env.REACT_APP_API_URL;

const configureAxios = () => {
    setBaseUrl();
    createRefreshTokenInterceptor();
}

const setBaseUrl = () => {
    axios.defaults.baseURL = api_url;
}

const createRefreshTokenInterceptor = () => {
    axios.interceptors.response.use(
        (response) => {
            return response;
        },
        function (error) {
            const originalRequest = error.config;
            if (error.response.status === 401 && !originalRequest._retry) {
                originalRequest._retry = true;

                return axios.post(api_url + "token/refreshToken", {}, { withCredentials: true })
                    .then((res) => {
                        if (res.status === 200) {
                            localStorage.setItem("token", res.data.token);

                            axios.defaults.headers.common["Authorization"] = "Bearer " + res.data.token;
                            originalRequest.headers["Authorization"] = "Bearer " + res.data.token;

                            return axios(originalRequest);
                        }
                    })
                    .catch((err) => {
                        localStorage.removeItem("token");
                        window.location = '/login';
                        return Promise.reject(error);
                    })
            }

            return Promise.reject(error);
        }
    );
};

configureAxios();

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
