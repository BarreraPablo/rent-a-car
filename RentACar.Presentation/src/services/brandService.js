import axios from 'axios';

export const getAll = () => {

    return axios.get('brand').then(res => {
        return Promise.resolve(res.data);
    }).catch(err => Promise.reject('Something gone wrong getting the brands.'));
}


export const save = (brand, action) => {


    return axios({
        method: action === "new" ? "post" : "put",
        url: "brand",
        data: brand,
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

