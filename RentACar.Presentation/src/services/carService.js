import axios from "axios";

const api_url = process.env.REACT_APP_API_URL;

export const getCars = (onlyAvailable = false) => {
    return axios
        .get(api_url + "car", {params: onlyAvailable})
        .then((res) => {
            return Promise.resolve(res.data);
        })
        .catch((err) => {
            return Promise.reject('Something gone wrong getting the cars');
        });
};

export const getById = (id) => {
    return axios
        .get(api_url + `car/${id}`)
        .then((res) => {
            return Promise.resolve(res.data);
        })
        .catch((err) => {
            return Promise.reject("Something gone wrong getting the car data");
        });
};

export const saveCar = (car, action) => {
    console.log("desde cars service, car", car);
    const formdata = new FormData();

    formdata.append("id", car.id);
    formdata.append("model", car.model);
    formdata.append("brandId", car.brandId);
    formdata.append("doors", car.doors);
    formdata.append("airConditioner", car.airConditioner);
    formdata.append("pricePerDay", car.pricePerDay);
    formdata.append("gearbox", car.gearbox);
    formdata.append("year", car.year);
    formdata.append("bodyTypeId", car.bodyTypeId);
    formdata.append("seats", car.seats);
    formdata.append("available", car.available);
    formdata.append("image", car.image);

    console.log('cars service, car:', car, 'action', action);
    return axios({
        method: action === "new" ? "post" : "put",
        url: api_url + "car",
        data: formdata,
    })
        .then(() => Promise.resolve())
        .catch((err) => Promise.reject("Something gone wrong saving the car.", err));
};
