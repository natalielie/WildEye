using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Request
{
    public class ReportRequestModel
    {
        public int EnterpriseId { get; set; }
        public int TaxId { get; set; }
        public string Comment { get; set; }
    }
}
