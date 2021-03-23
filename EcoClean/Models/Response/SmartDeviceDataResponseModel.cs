using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Response
{
    public class SmartDeviceDataResponseModel
    {
        public int EnterpriseId { get; set; }
        public double AirPollution { get; set; }
        public double WaterPollution { get; set; }
        public int AirPollutionSubstance { get; set; }
        public int WaterPollutionSubstance { get; set; }
        public double AirEmissions { get; set; }
        public double WaterEmissions { get; set; }
        public DateTime SmartDeviceDataDate { get; set; }
        public SmartDeviceDataResponseModel(int enterpriseId,
            double airPollution, double waterPollution, int airPollutionSubstance,
            int waterPollutionSubstance, double airEmissions, double waterEmissions, 
               DateTime smartDeviceDataDate)
        {
            this.EnterpriseId = enterpriseId;
            this.AirPollution = airPollution;
            this.WaterPollution = waterPollution;
            this.AirEmissions = airEmissions;
            this.WaterEmissions = waterEmissions;
            this.AirPollutionSubstance = airPollutionSubstance;
            this.WaterPollutionSubstance = waterPollutionSubstance;
            this.SmartDeviceDataDate = smartDeviceDataDate;
        }
    }
}
