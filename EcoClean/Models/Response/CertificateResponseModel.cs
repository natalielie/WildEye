using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Response
{
    public class CertificateResponseModel
    {
        public int EnterpriseId { get; set; }
        public int CertificateId { get; set; }
        public DateTime CertificateDate { get; set; }
        public CertificateResponseModel(int enterpriseId, int certificateId,
               DateTime certificateDate)
        {
            this.EnterpriseId = enterpriseId;
            this.CertificateId = certificateId;
            this.CertificateDate = certificateDate;
        }
    }
}
