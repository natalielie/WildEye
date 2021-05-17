import Axios from 'axios';
import authService from '../api-authorization/AuthorizeService'

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


    static deleteCertificateById = async (id, callback) => {
        const token = await authService.getAccessToken();

        Axios.delete('api/client/deleteCertificateById' + id, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        }))
            .then(() => TaxApi.getAllCertificates(callback))
            .catch(TaxApi.errorHandler);
    }

    errorHandler = error => console.log(error);

}


export default TaxApi;