import Axios from 'axios';

class ReportsApi {

    static getAllReports = (callback) => {
        Axios.get('api/admin/getAllReports')
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


    static deleteCertificateById = (id, callback) => {
        Axios.delete('api/client/deleteCertificateById' + id)
            .then(() => ReportsApi.getAllReports(callback))
            .catch(ReportsApi.errorHandler);
    }

    errorHandler = error => console.log(error);

}


export default ReportsApi;