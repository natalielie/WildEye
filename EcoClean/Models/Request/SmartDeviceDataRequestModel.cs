using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Request
{
    public class SmartDeviceDataRequestModel
    {
        public int EnterpriseId { get; set; }
        public double AirPollution { get; set; }
        public double WaterPollution { get; set; }
        public DateTime SmartDeviceDataDate { get; set; }
    }
}
