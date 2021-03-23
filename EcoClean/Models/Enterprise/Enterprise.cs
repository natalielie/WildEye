using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Enterprise
{
    public class Enterprise
    {
        public int EnterpriseId { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string PhoneNumber { get; set; }
        public string Product { get; set; }
        public string Address { get; set; }
        public double Rate { get; set; }
        public int ClientId { get; set; }

        public Enterprise() { }
        public Enterprise(int enterpriseId, string name,
            string kind, string phoneNumber, string product,
            string address, double rate, int clientId)
        {
            this.EnterpriseId = enterpriseId;
            this.Name = name;
            this.Kind = kind;
            this.PhoneNumber = phoneNumber;
            this.Product = product;
            this.Address = address;
            this.Rate = Rate;
            this.ClientId = clientId;
        }

    }
}

