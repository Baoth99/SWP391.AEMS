using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AEMS.Data.Entities
{
    [Table("Photo")]
    public class RepairmentRequestDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("RepairmentRequest")]
        public Guid RepairmentRequestId { get; set; }

        public string Description { get; set; }

        public string Processing { get; set; }

    }
}
