import Axios from 'axios';
import authService from '../api-authorization/AuthorizeService'

class ReportsApi {

    static getAllReports = (callback) => {
        Axios.get('api/admin/getAllReports')
            .then(res => callback(res.data))
            .catch(ReportsApi.errorHandler);
    }

    static getAllUsersReports = async (callback) => {
        const token = await authService.getAccessToken();
        Axios.get('api/client/getAllUsersReports', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        })
            .then(res => callback(res.data))
            .catch(ReportsApi.errorHandler);
    }

    static getAllReportsOfEnterprise = (enterpriseId, callback) => {
        Axios.get('api/admin/getAllReportsOfEnterprise', enterpriseId)
            .then(res => callback(res.data))
            .catch(ReportsApi.errorHandler);
    }

    static GetSingleReportOfEnterprise = (reportId, callback) => {
        Axios.get('api/client/getSingleReportOfEnterprise/' + reportId)
            .then(res => callback(res.data))
            .catch(ReportsApi.errorHandler);
    }

    static createReport = (report, callback) => {
        Axios.post('api/client/createReport/', report)
            .then(() => ReportsApi.getAllReports(callback))
            .catch(ReportsApi.errorHandler);
    }

    static deleteReportById = (id, callback) => {
        Axios.delete('api/client/deleteReportById/' + id)
            .then(() => ReportsApi.getAllReports(callback))
            .catch(ReportsApi.errorHandler);
    }


    static getAllTaxes = async (callback) => {
        const token = await authService.getAccessToken();

        Axios.get('api/client/getAllTaxes', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        })
            .then(res => callback(res.data))
            .catch(ReportsApi.errorHandler);
    }


    errorHandler = error => console.log(error);

}


export default ReportsApi;