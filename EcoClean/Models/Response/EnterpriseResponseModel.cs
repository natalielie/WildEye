using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Response
{
    public class EnterpriseResponseModel
    {
        public int EnterpriseId { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string PhoneNumber { get; set; }
        public string Product { get; set; }
        public string Address { get; set; }
        public double Rate { get; set; }
        public int ClientId { get; set; }

        public EnterpriseResponseModel(int enterpriseId, string name,
            string kind, string phoneNumber, string product, string address, double rate)
        {
            this.EnterpriseId = enterpriseId;
            this.Name = name;
            this.Kind = kind;
            this.PhoneNumber = phoneNumber;
            this.Product = product;
            this.Address = address;
            this.Rate = rate;
        }
    }
}
