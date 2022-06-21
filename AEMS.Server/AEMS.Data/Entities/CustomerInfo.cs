using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AEMS.Data.Entities
{
    public class CustomerInfo
    {
        public string Id { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        [ForeignKey("Area")]
        public Guid? AreaId { get; set; }
    }
}
