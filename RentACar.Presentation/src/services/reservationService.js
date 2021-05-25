import axios from "axios";

export const getAll = () => {
    return axios
        .get("reservation")
        .then((res) => Promise.resolve(res.data))
        .catch((err) =>
            Promise.reject("Something gone wrong getting the reservations.")
        );
};

export const getById = (id) => {
    return axios
        .get(`reservation/${id}`)
        .then((res) => Promise.resolve(res.data))
        .catch((err) =>
            Promise.reject("Something gone wrong getting the reservation")
        );
};

export const save = (reservation, action) => {
    return axios({
        method: action === "new" ? "post" : "put",
        url: "reservation",
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

export const finish = (id) => {
    return axios.put(`reservation/finish/${id}`).catch(err => {
        if(err.response && err.response.status === 400) {
            return Promise.reject(err.response.data.bussinessValidation[0]);
        }

        return Promise.reject("Something gone wrong finishing the car")
    })
}
