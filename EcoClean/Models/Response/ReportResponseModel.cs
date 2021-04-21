using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Response
{
    public class ReportResponseModel
    {
        public int ReportId { get; set; }
        public string EnterpriseName { get; set; }
        public int AirPollutionSubstance { get; set; }
        public int WaterPollutionSubstance { get; set; }
        public double AirEmissions { get; set; }
        public double WaterEmissions { get; set; }
        public double TaxCost { get; set; }
        public string Comment { get; set; }
        public DateTime ReportDate { get; set; }

        public ReportResponseModel() { }
        public ReportResponseModel(int reportId, string enterpriseName,
            int airPollutionSubstance, int waterPollutionSubstance, 
            double airEmissions, double waterEmissions, 
            double taxCost, string comment, DateTime reportDate)
        {
            this.ReportId = reportId;
            this.EnterpriseName = enterpriseName;
            this.AirEmissions = airEmissions;
            this.AirPollutionSubstance = airPollutionSubstance;
            this.WaterPollutionSubstance = waterPollutionSubstance;
            this.WaterEmissions = waterEmissions;
            this.TaxCost = taxCost;
            this.Comment = comment;
            this.ReportDate = reportDate;
        }
    }
}
