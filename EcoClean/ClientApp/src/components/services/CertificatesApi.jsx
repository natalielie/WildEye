import Axios from 'axios';
import authService from '../api-authorization/AuthorizeService'

class CertificatesApi {

    static getAllCertificates = (callback) => {
        Axios.get('api/admin/getAllCertificates')
            .then(res => callback(res.data))
            .catch(CertificatesApi.errorHandler);
    }

    static getAllUsersCertificates = async (callback) => {
        const token = await authService.getAccessToken();
        Axios.get('api/client/getAllUsersCertificates', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        })
            .then(res => callback(res.data))
            .catch(CertificatesApi.errorHandler);
    }

    static createCertificate = (certificate, callback) => {
        Axios.post('api/client/createCertificate/', certificate)
            .then(() => CertificatesApi.getAllCertificates(callback))
            .catch(CertificatesApi.errorHandler);
    }


    //static editAppointment = (appointment, callback) => {
    //    let id = appointment.appointmentId;
    //    delete appointment.appointmentId;
    //    Axios.put('api/doctor/changeAppointment/' + id, appointment)
    //        .then(() => AppointmentApi.getAllAppointments(callback))
    //        .catch(AppointmentApi.errorHandler);
    //}


    static deleteCertificateById = (id, callback) => {
        Axios.delete('api/client/deleteCertificateById/' + id)
            .then(() => CertificatesApi.getAllCertificates(callback))
            .catch(CertificatesApi.errorHandler);
    }

    errorHandler = error => console.log(error);

}


export default CertificatesApi;