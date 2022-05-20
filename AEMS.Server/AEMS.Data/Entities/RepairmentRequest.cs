using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AEMS.Data.Entities
{
    [Table("Photo")]
    public class RepairmentRequest : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid? EquipmentInspectionId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        public string Description { get; set; }
    }
}
