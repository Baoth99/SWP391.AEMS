using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AEMS.Data.Entities
{
    [Table("EquipmentLog")]
    public class EquipmentLog
    {
        [Key]
        public string Id { get; set; } // Device Code

        [Key]
        public DateTime EventTime { get; set; }

        [Key]
        public DateTime CreatedAt { get; set; }

        public DateTime ReceivedAt { get; set; }

        public int EventType { get; set; }

        public int Status { get; set; }

        public string Message { get; set; }
    }
}
