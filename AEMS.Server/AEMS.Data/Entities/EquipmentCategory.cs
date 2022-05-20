using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AEMS.Data.Entities
{
    [Table("EquipmentCategory")]
    public class EquipmentCategory : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}
