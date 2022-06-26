using AEMS.MSAzureService;
using System;
using System.Collections.Generic;

namespace AEMS.Application
{
    public class DeviceLogQuery : IQuery<IEnumerable<DeviceLogViewModel>>
    {
        public string DeviceCode { get; set; }
    }

    public class DeviceLogViewModel : BaseCosmosDbModel
    {
        public string id { get ; set; }

        public string DeviceId { get; set; } // Device Code

        public DateTime EventTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ReceivedAt { get; set; }

        public int EventType { get; set; }

        public int Status { get; set; } //Connected=0  //Standby=1 //Disconnected=2

        public string StatusMessage { get; set; }  //Connected=0  //Standby=1 //Disconnected=2

        public string Message { get; set; }

        public float BatteryLevel { get; set; }

        public float Power { get; set; } //W

        public float Energy { get; set; } //kWh

        public float Temperature { get; set; } // C

        public float Voltage { get; set; } // V

        public string MeterType { get; set; }

        public float Version { get; set; }

        // Geolocation
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public decimal Altitude { get; set; }


        public decimal Magnetometer_x { get; set; }

        public decimal Magnetometer_y { get; set; }

        public decimal Magnetometer_z { get; set; }
    }
}
