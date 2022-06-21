using System;

namespace AEMS.Monitors
{
    public class AEMSDeviceMessageRequestModel
    {
        public string DeviceId { get; set; } // Device ID

        public DateTime CreatedAt { get; set; }

        public DateTime EventTime { get; set; }

        public int EventType { get; set; }

        public int Status { get; set; } //Connected=0  //Standby=1 //Disconnected=2

        public string StatusMessage { get; set; }  //Connected=0  //Standby=1 //Disconnected=2

        public string Message { get; set; }

        public float BatteryLevel { get; set; }

        public float Power { get; set; } //W

        public float Energy { get; set; } //kWh

        public float Voltage { get; set; } // V

        public float Temperature { get; set; } // C

        public Location Geolocation { get; set; }

        public string MeterType { get; set; }

        public Coordinate Magnetometer { get; set; }

        public float Version { get; set; }

    }

    public class Coordinate
    {
        public decimal x { get; set; }

        public decimal y { get; set; }

        public decimal z { get; set; }
    }

    public class Location
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public decimal Altitude { get; set; }
    }
}
