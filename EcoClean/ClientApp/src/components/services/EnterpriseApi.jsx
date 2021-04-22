import Axios from 'axios';

class EnterpriseApi {

    static getAllEnterprises = (callback) => {
        Axios.get('api/client/getAllEnterprises')
            .then(res => callback(res.data))
            .catch(EnterpriseApi.errorHandler);
    }

    static getMyEnterprise = (enterpriseId, callback) => {
        Axios.get('api/client/GetEnterpriseById/'+ enterpriseId)
            .then(res => callback(res.data))
            .catch(EnterpriseApi.errorHandler);
    }

    static getAllEnterprisesInSystem = (callback) => {
        Axios.get('api/client/getAllEnterprisesInSystem')
            .then(res => callback(res.data))
            .catch(EnterpriseApi.errorHandler);
    }

    static addEnterprise = (enterprise, callback) => {
        Axios.post('api/client/addEnterprise/', enterprise)
            .then(() => EnterpriseApi.getAllEnterprises(callback))
            .catch(EnterpriseApi.errorHandler);
    }


    static DeleteEnterpriseById = (id, callback) => {
        Axios.delete('api/client/DeleteEnterpriseById' + id)
            .then(() => EnterpriseApi.getAllEnterprises(callback))
            .catch(EnterpriseApi.errorHandler);
    }

    errorHandler = error => console.log(error);

}


export default EnterpriseApi;