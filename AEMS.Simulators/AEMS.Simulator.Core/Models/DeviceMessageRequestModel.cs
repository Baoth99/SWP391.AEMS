using System;

namespace AEMS.Simulator.Core
{
    public class DeviceMessageRequestModel
    {
        public string DeviceId { get; set; } 

        public DateTime EventTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public int EventType { get; set; }

        public int Status { get; set; } //Connected=0  //Standby=1 //Disconnected=2

        public string StatusMessage { get; set; }  //Connected=0  //Standby=1 //Disconnected=2

        public string Message { get; set; }

        public long BatteryLevel { get; set; }

        public float Power { get; set; } //W

        public float Energy { get; set; } //kWh

        public float Voltage { get; set; } // V

        public float Temperature { get; set; } // C
        public Location Geolocation { get; set; }

        public string MeterType { get; set; }

        public Coordinate Magnetometer { get; set; }

        public float Version { get; set; }


        public static DeviceMessageRequestModel CreateRequestModel(string DeviceCode)
        {
            return new DeviceMessageRequestModel()
            {
                DeviceId = DeviceCode,
                CreatedAt = DateTimeCountry.DateTimeNow(),
                EventTime = DateTimeCountry.DateTimeNow(),
                EventType = 1,
                Message = $"{DeviceCode} is runing very well",
                Status = DeviceStatus.Standby,
                StatusMessage = DeviceStatusMesage.Standby,
                BatteryLevel = 80,
                Energy = (float)81.63,
                Power = (float)68.32,
                Voltage = (float)128.51,
                Temperature = (float)40.0,

                Geolocation = new Location()
                {
                    Latitude = (decimal)10.777786761721552,
                    Longitude = (decimal)106.62406201837359,
                    Altitude = (decimal)6.5
                },
                Magnetometer = new Coordinate()
                {
                    x = (decimal)40.21,
                    y = (decimal)-112.67,
                    z = (decimal)-40.24
                },
                MeterType = "3S",
                Version = 1.0F,
            };
        }

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
