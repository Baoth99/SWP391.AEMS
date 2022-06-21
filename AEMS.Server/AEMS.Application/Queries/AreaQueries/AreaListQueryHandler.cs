using AEMS.Data.EF.UnitOfWork;
using AEMS.ORM.Dapper;
using AEMS.Utilities;
using Microsoft.ApplicationInsights;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AEMS.Application
{
    public class AreaListQueryHandler : BaseDataAccess, IQueryHandler<AreaListQuery, IEnumerable<AreaViewModel>>
    {
        public AreaListQueryHandler(IUnitOfWork unitOfWork, IDapperService dapperService, IAuthSession authSession, TelemetryClient telemetryClient) : base(unitOfWork, dapperService, authSession, telemetryClient)
        {
        }

        public async Task<IEnumerable<AreaViewModel>> Handle(AreaListQuery request, CancellationToken cancellationToken)
        {
            var dataQuery = await UnitOfWork.AreaRepository.GetAllAsNoTracking().Select(x => new AreaViewModel()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();

            return dataQuery;
        }
    }
}
