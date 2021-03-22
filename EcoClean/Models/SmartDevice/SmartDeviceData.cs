using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.SmartDevice
{
    public class SmartDeviceData
    {
        public int SmartDeviceDataId { get; set; }
        public int EnterpriseId { get; set; }
        public float AirPollution { get; set; }
        public float WaterPollution { get; set; }
        public DateTime SmartDeviceDataDate { get; set; }
    }
}
