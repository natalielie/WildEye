using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Enterprise
{
    public class Certificate
    {
        public int CertificateId { get; set; }
        public int EnterpriseId { get; set; }
        public DateTime CertificateDate { get; set; }
        public Certificate(){ }

        public Certificate(int certificateId, int enterpriseId, DateTime certificateDate) 
        {
            this.CertificateId = certificateId;
            this.EnterpriseId = enterpriseId;
            this.CertificateDate = certificateDate;
        }
    }
}
