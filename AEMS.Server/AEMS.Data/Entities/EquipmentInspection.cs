using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AEMS.Data.Entities
{
    [Table("EquipmentInspection")]
    public class EquipmentInspection : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid? Inspector { get; set; }

        public string InspectionType { get; set; }

        public Guid? Manager { get; set; }

        public Guid? EquipmentId { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }
    }
}
