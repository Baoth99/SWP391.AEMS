using AEMS.Data.EF.UnitOfWork;
using AEMS.DataAccess.DTOs;
using AEMS.DataAccess.Models;
using AEMS.ORM.Dapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AEMS.DataAccess.Queries
{
    public class EquipmentQueryHandler : BaseDataAccess, IQueryHandler<GetEquipmentListQuery, IEnumerable<EquipmentViewModel>>
    {
        public EquipmentQueryHandler(IUnitOfWork unitOfWork, IDapperService dapperService) : base(unitOfWork, dapperService)
        {

        }

        public async Task<IEnumerable<EquipmentViewModel>> Handle(GetEquipmentListQuery request, CancellationToken cancellationToken)
        {
            var resDemo = new List<EquipmentViewModel>()
            {
                new EquipmentViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name 01",
                    Code = "Code 01"
                },
                new EquipmentViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name 02",
                    Code = "Code 02"
                },
                new EquipmentViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name 03",
                    Code = "Code 03"
                },
            };

            return resDemo;
        }
    }
}
