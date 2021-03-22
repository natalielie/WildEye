using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Request
{
    public class EnterpriseRequestModel
    {
        public int EnterpriseId { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string PhoneNumber { get; set; }
        public string Product { get; set; }
        public string Address { get; set; }
        public double Rate { get; set; }
        public int ClientId { get; set; }
    }
}
