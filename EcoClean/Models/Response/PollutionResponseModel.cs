using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Response
{
    public class PollutionResponseModel
    {
        public double AirPollutionAverage { get; set; }
        public double WaterPollutionAverage {get; set;}

        public PollutionResponseModel() {  }
        public PollutionResponseModel(double airPollutionAverage,
            double waterPollutionAverage)
        {
            this.AirPollutionAverage = airPollutionAverage;
            this.WaterPollutionAverage = waterPollutionAverage;
        }
    }
}
