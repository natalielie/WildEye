import Axios from 'axios';

class StatisticsApi {

    static getStatistics = (callback) => {
        Axios.get('api/client/getStatistics')
            .then(res => callback(res.data))
            .catch(StatisticsApi.errorHandler);
    }

    static getReversedStatistics = (callback) => {
        Axios.get('api/client/getStatistics')
            .then(res => callback(res.data))
            .catch(StatisticsApi.errorHandler);
    }

    //static getTop = (callback) => {
    //    Axios.get('api/client/getTop')
    //        .then(res => callback(res.data))
    //        .catch(StatisticsApi.errorHandler);
    //}

    errorHandler = error => console.log(error);

}


export default StatisticsApi; 