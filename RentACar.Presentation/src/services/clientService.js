import axios from "axios";

export const getAll = () => {
    return axios
        .get("client")
        .then((res) => Promise.resolve(res.data))
        .catch((err) =>
            Promise.reject("Something gone wrong getting the clients")
        );
};

export const getById = (id) => {
    return axios
        .get(`client/${id}`)
        .then((res) => Promise.resolve(res.data))
        .catch((err) =>
            Promise.reject("Something gone wrong getting the clients")
        );
};

export const save = (formdata, action) => {
    return axios({
        method: action === "new" ? "post" : "put",
        url: "client",
        data: formdata,
    }).catch((err) => {
        if (err.response && err.response.status === 400) {
            return Promise.reject(err.response.data);
        }
        return Promise.reject("Something gone wrong saving the reservation.");
    });
};
