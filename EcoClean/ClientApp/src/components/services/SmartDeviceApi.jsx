import Axios from 'axios';
import authService from '../api-authorization/AuthorizeService'

class SmartDeviceApi {

    static getAllData = async (callback) => {
        const token = await authService.getAccessToken();
        Axios.get('api/client/getAllData', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        })
            .then(res => callback(res.data))
            .catch(SmartDeviceApi.errorHandler);
    }

    static getAllDataInSystem = (callback) => {
        Axios.get('api/client/getAllDataInSystem')
            .then(res => callback(res.data))
            .catch(SmartDeviceApi.errorHandler);
    }

    static addSmartDeviceData = (smartdata, callback) => {
        Axios.post('/api/admin/addSmartData', smartdata)
            .then(() => SmartDeviceApi.getAllData(callback))
            .catch(SmartDeviceApi.errorHandler);
    }

    errorHandler = error => console.log(error);

}


export default SmartDeviceApi;