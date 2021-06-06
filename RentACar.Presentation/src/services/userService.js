import axios from 'axios'
import { message } from "antd";


export const register = (user) => {

    return axios.post('user', user).catch(err => {
        if(err.response.status === 400) {
            console.log(err.response)
            if(err.response.data.bussinessValidation){
                const error = {}
                switch(err.response.data.bussinessValidation[0]){
                    case "The username is already taken":
                        error.username = "The username is already taken";
                        break;
                    case "There is already an account with this email address":
                        error.emailAddress = "There is already an account with this email address";
                        break;
                    default:
                        message.error('An unexpected error ocurred');
                        return Promise.reject(null);
                }
                return Promise.reject(error);
            } else {
                return Promise.reject(err.response.data);
            }
        } else {
            message.error('An unexpected error ocurred');
            return Promise.reject(null);
        }
    });
}

export const startPasswordRecovery = (emailAddress) => {

    return axios.post('user/startpasswordrecovery', {EmailAddress: emailAddress}).catch(err => {
        message.error('An unexpected error ocurred');

        return Promise.reject();
    })
}

export const recoverPassword = (password, recoveryToken) => {

    return axios.post('user/recoverpassword', {password: password, recoveryToken: recoveryToken}).catch(err => {
        if(err.response.status === 400) {
            return Promise.reject(err.response.data);
        } 

        message.error('An unexpected error ocurred')
        return Promise.reject(null);
    })
}