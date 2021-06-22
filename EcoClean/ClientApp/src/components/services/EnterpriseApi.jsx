import Axios from 'axios';
import authService from '../api-authorization/AuthorizeService'

class EnterpriseApi {

    
    static getAllEnterprises = async (callback) => {
        const token = await authService.getAccessToken();

        Axios.get('api/client/getAllEnterprises', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
            })
            .then(res => callback(res.data))
            .catch(EnterpriseApi.errorHandler);
    }


    static getMyEnterprise = async (enterpriseId, callback) => {
        const token = await authService.getAccessToken();

        Axios.get('api/client/GetEnterpriseById/' + enterpriseId, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        })
            .then(res => callback(res.data))
            .catch(EnterpriseApi.errorHandler);
    }

    static getAllEnterprisesInSystem = async (callback) => {
        const token = await authService.getAccessToken();
        Axios.get('api/client/getAllEnterprisesInSystem/', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        })
            .then(res => callback(res.data))
            .catch(EnterpriseApi.errorHandler);
    }

    static addEnterprise = async (enterprise, callback) => {
        const token = await authService.getAccessToken();

        Axios.post('api/client/addEnterprise/', enterprise, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        })
            .then(() => EnterpriseApi.getAllEnterprises(callback))
            .catch(EnterpriseApi.errorHandler);
    }


    static DeleteEnterpriseById = async (id, callback) => {
        const token = await authService.getAccessToken();
        Axios.delete('api/client/DeleteEnterpriseById/' + id, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        })
            .then(() => EnterpriseApi.getAllEnterprises(callback))
            .catch(EnterpriseApi.errorHandler);
    }

    static getAllEnterprisesForDrop = async (callback) => {
        const token = await authService.getAccessToken();

        Axios.get('api/client/getAllEnterprises', {

            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        })
            .then(res => callback(res.data))
            .catch(EnterpriseApi.errorHandler);
    }


    errorHandler = error => console.log(error);

}


export default EnterpriseApi;