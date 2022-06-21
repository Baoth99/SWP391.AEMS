using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AEMS.Data.Entities
{
    [Table("Device")]
    public class Device : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string Imei { get; set; }

        public string BusinessVersion { get; set; }

        public string FirmwareVersion { get; set; }

        public string ManagementVersion { get; set; }

        public string Description { get; set; }

        public string PowerBiDashboardId { get; set; }

        public string PowerBiDashboardEndpoint { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int Status { get; set; }

        [ForeignKey("DeviceCategory")]
        public Guid DeviceCategoryId { get; set; }

        [ForeignKey("Area")]
        public Guid AreaId { get; set; }

    }
}
