using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Enterprise
{
    public class Report
    {
        public int ReportId { get; set; }
        public int EnterpriseId { get; set; }
        public int TaxId { get; set; }
        public string Comment { get; set; }
        public DateTime ReportDate { get; set; }
    }
}
