import Axios from 'axios';

class ReportsApi {

    static getAllReports = (callback) => {
        Axios.get('api/admin/getAllReports')
            .then(res => callback(res.data))
            .catch(ReportsApi.errorHandler);
    }

    static getAllUsersReports = (callback) => {
        Axios.get('api/client/getAllUsersReports')
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
        Axios.post('api/admin/createReport/', report)
            .then(() => ReportsApi.getAllReports(callback))
            .catch(ReportsApi.errorHandler);
    }

    //static editAppointment = (appointment, callback) => {
    //    let id = appointment.appointmentId;
    //    delete appointment.appointmentId;
    //    Axios.put('api/doctor/changeAppointment/' + id, appointment)
    //        .then(() => AppointmentApi.getAllAppointments(callback))
    //        .catch(AppointmentApi.errorHandler);
    //}


    static deleteReportById = (id, callback) => {
        Axios.delete('api/client/deleteReportById' + id)
            .then(() => ReportsApi.getAllReports(callback))
            .catch(ReportsApi.errorHandler);
    }


    static getAllTaxes = (callback) => {
        Axios.get('api/client/getAllTaxes')
            .then(res => callback(res.data))
            .catch(ReportsApi.errorHandler);
    }


    errorHandler = error => console.log(error);

}


export default ReportsApi;