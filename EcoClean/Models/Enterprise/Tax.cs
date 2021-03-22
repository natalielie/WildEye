using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Enterprise
{
    public class Tax
    {

        public int TaxId { get; set; }
        public int EnterpriseId { get; set; }
        public int AirPollutionSubstance { get; set; }
        public int WaterPollutionSubstance { get; set; }
        public double AirEmissions { get; set; }
        public double WaterEmissions { get; set; }
        public double TaxCost { get; set; }

        public Tax(int enterpriseId,int airPollutionSubstance,
            int waterPollutionSubstance, double airEmissions, double waterEmissions, double taxCost)
        {
            this.EnterpriseId = enterpriseId;
            this.AirEmissions = airEmissions;
            this.AirPollutionSubstance = airPollutionSubstance;
            this.WaterPollutionSubstance = waterPollutionSubstance;
            this.WaterEmissions = waterEmissions;
            this.TaxCost = taxCost;
        }
    }
}
