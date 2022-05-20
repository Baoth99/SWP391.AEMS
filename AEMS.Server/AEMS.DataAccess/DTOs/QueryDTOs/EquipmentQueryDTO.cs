using AEMS.DataAccess.Models;
using System.Collections.Generic;

namespace AEMS.DataAccess.DTOs
{
    public class GetEquipmentListQuery : IQuery<IEnumerable<EquipmentViewModel>>
    {
        public string Name { get; set; }
        
    }
}
