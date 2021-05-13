import axios from "axios";

const api_url = process.env.REACT_APP_API_URL;

export const getAll = () => {
    return axios
        .get(api_url + "bodyType")
        .then((res) => Promise.resolve(res.data))
        .catch(err => Promise.reject('Something gone wrong getting the body types'));
};
