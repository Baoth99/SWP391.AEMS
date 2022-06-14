using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AEMS.Data.Entities
{
    [Table("DeviceStatus")]
    public class DeviceStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Device")]
        public Guid DeviceId { get; set; }

        public int Status { get; set; }

        public string StatusMessage { get; set; }

        public string Reason { get; set; }

        public DateTime AtTime { get; set; }

    }
}
