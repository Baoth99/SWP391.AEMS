using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AEMS.Data.Entities
{
    [Table("EquipmentInspectionDetail")]
    public class EquipmentInspectionDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("EquipmentInspection")]
        public Guid EquipmentInspectionId { get; set; }

        public string Situation { get; set; }

        public string Error { get; set; }

        public string Solution { get; set; }

    }
}
