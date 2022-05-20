using System;

namespace AEMS.Data
{
    public class BaseEntity
    {
        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        public Guid? ModifedBy { get; set; }
    }
}
