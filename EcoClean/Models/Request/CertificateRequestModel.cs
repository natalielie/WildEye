using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Request
{
    public class CertificateRequestModel
    {
        public int EnterpriseId { get; set; }
        public DateTime CertificateDate { get; set; }
    }
}
