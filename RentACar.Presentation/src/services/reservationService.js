import axios from "axios";

const api_url = process.env.REACT_APP_API_URL;

export const getAll = () => {
    return axios
        .get(api_url + "reservation")
        .then((res) => Promise.resolve(res.data))
        .catch((err) =>
            Promise.reject("Something gone wrong getting the reservations.")
        );
};

export const getById = (id) => {
    return axios
        .get(api_url + `reservation/${id}`)
        .then((res) => Promise.resolve(res.data))
        .catch((err) =>
            Promise.reject("Something gone wrong getting the reservation")
        );
};

export const save = (reservation, action) => {
    return axios({
        method: action === "new" ? "post" : "put",
        url: api_url + "reservation",
        data: reservation,
    }).then(res => Promise.resolve(res.data)).catch(err => {
        if (err.response && err.response.status === 400) {
            return Promise.reject(err.response.data);
        }
        return Promise.reject(
            "Something gone wrong saving the reservation."
        );

    })
};
