using System;

namespace AEMS.Monitors
{
    public class DeviceLogPowerBiDashboardModel
    {
        public string deviceId { get; set; } // Device Code

        public DateTime eventTime { get; set; }

        public DateTime receivedAt { get; set; }

        public int eventType { get; set; }

        public int Status { get; set; } //Connected=0  //Standby=1 //Disconnected=2

        public string statusMessage { get; set; }  //Connected=0  //Standby=1 //Disconnected=2

        public string message { get; set; }

        public float batteryLevel { get; set; }

        public float power { get; set; } //W

        public float energy { get; set; } //kWh

        public float temperature { get; set; } // C

        public float voltage { get; set; } // V

        public string meterType { get; set; }

        public float version { get; set; }

        // Geolocation
        public decimal latitude { get; set; }

        public decimal longitude { get; set; }

        public decimal altitude { get; set; }


        public decimal magnetometer_x { get; set; }

        public decimal magnetometer_y { get; set; }

        public decimal magnetometer_z { get; set; }
    }
}
