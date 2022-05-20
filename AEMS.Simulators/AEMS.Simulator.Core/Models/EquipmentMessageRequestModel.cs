using System;

namespace AEMS.Simulator.Core
{
    public class EquipmentMessageRequestModel
    {
        public string Id { get; set; } // Device Code

        public DateTime EventTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public int EventType { get; set; }

        public int Status { get; set; }

        public string Message { get; set; }
    }
}
