import axios from "axios";

const api_url = process.env.REACT_APP_API_URL;

export const getAll = () => {
    return axios
        .get(api_url + "client")
        .then((res) => Promise.resolve(res.data))
        .catch((err) =>
            Promise.reject("Something gone wrong getting the clients")
        );
};

export const getById = (id) => {
    return axios
        .get(api_url + `client/${id}`)
        .then((res) => Promise.resolve(res.data))
        .catch((err) =>
            Promise.reject("Something gone wrong getting the clients")
        );
};

export const save = (formdata, action) => {
    return axios({
        method: action === "new" ? "post" : "put",
        url: api_url + "client",
        data: formdata,
    }).catch((err) => {
        if (err.response && err.response.status === 400) {
            return Promise.reject(err.response.data);
        }
        return Promise.reject("Something gone wrong saving the reservation.");
    });
};
