import axios from "axios";

export const getAll = () => {
    return axios
        .get("country")
        .then((res) => Promise.resolve(res.data))
        .catch((err) =>
            Promise.reject("Something gone wrong getting the countries")
        );
};
