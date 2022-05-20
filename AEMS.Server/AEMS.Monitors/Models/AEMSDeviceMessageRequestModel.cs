using System;

namespace AEMS.Monitors
{
    public class AEMSDeviceMessageRequestModel
    {
        public string Id { get; set; } // Device ID

        public DateTime EventTime { get; set; }

        public DateTime CreatedAt { get; set; }


        public int EventType { get; set; }

        public int Status { get; set; }

        public string Message { get; set; }
    }
}
