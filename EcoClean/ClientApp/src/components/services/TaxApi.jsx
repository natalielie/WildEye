import Axios from 'axios';

class TaxApi {

    //static getAllTaxes = (callback) => {
    //    Axios.get('api/admin/getAllTaxes')
    //        .then(res => callback(res.data))
    //        .catch(TaxApi.errorHandler);
    //}

    static countTax = (taxInfo, callback) => {
        Axios.post('api/client/countTax/', taxInfo)
            .then(() => TaxApi.getAllCertificates(callback))
            .catch(TaxApi.errorHandler);
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
            .then(() => TaxApi.getAllCertificates(callback))
            .catch(TaxApi.errorHandler);
    }

    errorHandler = error => console.log(error);

}


export default TaxApi;