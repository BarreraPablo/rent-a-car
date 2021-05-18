import axios from "axios";

const api_url = process.env.REACT_APP_API_URL;

export const getAll = () => {
    return axios
        .get(api_url + "documentType")
        .then((res) => Promise.resolve(res.data))
        .catch((err) =>
            Promise.resolve("Something gone wrong getting the documents types.")
        );
};
