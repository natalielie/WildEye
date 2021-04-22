import Axios from 'axios';

class SmartDeviceApi {

    static getAllData = (callback) => {
        Axios.get('api/client/getAllData')
            .then(res => callback(res.data))
            .catch(SmartDeviceApi.errorHandler);
    }

    static getAllDataInSystem = (callback) => {
        Axios.get('api/client/getAllDataInSystem')
            .then(res => callback(res.data))
            .catch(SmartDeviceApi.errorHandler);
    }

    static addSmartDeviceData = (data, callback) => {
        Axios.post('api/client/addSmartDeviceData/', data)
            .then(() => SmartDeviceApi.getAllData(callback))
            .catch(SmartDeviceApi.errorHandler);
    }

    errorHandler = error => console.log(error);

}


export default SmartDeviceApi;