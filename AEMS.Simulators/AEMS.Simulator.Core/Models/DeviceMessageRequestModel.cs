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
                BatteryLevel = GenerateRandom.RandomNum(10,90),
                Energy = GenerateRandom.RandomNum(10, 90),
                Power = GenerateRandom.RandomNum(10, 90),
                Voltage = GenerateRandom.RandomNum(10, 90),
                Temperature = GenerateRandom.RandomNum(15, 70),

                Geolocation = new Location()
                {
                    Latitude = (decimal)10.777786761721552,
                    Longitude = (decimal)106.62406201837359,
                    Altitude = (decimal)6.5
                },
                Magnetometer = new Coordinate()
                {
                    x = GenerateRandom.RandomNum(-200, 200),
                    y = GenerateRandom.RandomNum(-200, 200),
                    z = GenerateRandom.RandomNum(-200, 200)
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
