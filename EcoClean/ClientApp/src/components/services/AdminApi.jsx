import Axios from 'axios';

class AdminApi {

    static getAveragePollutionData = (callback) => {
        Axios.get('api/admin/getAveragePollutionData')
            .then(res => callback(res.data))
            .catch(AdminApi.errorHandler);
    }

    static getEnterpriseRateById = (id, callback) => {
        Axios.get('api/admin/getAveragePollutionData/', id)
            .then(res => callback(res.data))
            .catch(AdminApi.errorHandler);
    }

    static setEnterpriseRate = (id, callback) => {
        Axios.post('api/admin/setEnterpriseRate/', id)
            .then(() => AdminApi.getAveragePollutionData(callback))
            .catch(AdminApi.errorHandler);
    }


    //static editAppointment = (appointment, callback) => {
    //    let id = appointment.appointmentId;
    //    delete appointment.appointmentId;
    //    Axios.put('api/doctor/changeAppointment/' + id, appointment)
    //        .then(() => AppointmentApi.getAllAppointments(callback))
    //        .catch(AppointmentApi.errorHandler);
    //}


    getEnterpriseRateById

    errorHandler = error => console.log(error);

}


export default AdminApi;