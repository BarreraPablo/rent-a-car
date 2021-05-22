import axios from "axios";

export const getAll = () => {
    return axios
        .get("documentType")
        .then((res) => Promise.resolve(res.data))
        .catch((err) =>
            Promise.resolve("Something gone wrong getting the documents types.")
        );
};

export const save = (documentType, action) => {


    return axios({
        method: action === "new" ? "post" : "put",
        url: "documentType",
        data: documentType,
    })
        .catch((err) => {
            if (err.response && err.response.status === 400) {
                if(err.response.data && err.response.data.name){
                    return Promise.reject(err.response.data.name[0]);
                }

                return Promise.reject("Please verify the information submitted.");
            }
            return Promise.reject("Something gone wrong saving the reservation.");
        });
}
