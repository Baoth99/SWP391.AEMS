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
    public class DeviceCategoryListQueryHandler : BaseDataAccess, IQueryHandler<DeviceCategoryListQuery, IEnumerable<DeviceCategoryViewModel>>
    {
        public DeviceCategoryListQueryHandler(IUnitOfWork unitOfWork, IDapperService dapperService, IAuthSession authSession, TelemetryClient telemetryClient) : base(unitOfWork, dapperService, authSession, telemetryClient)
        {
        }

        public async Task<IEnumerable<DeviceCategoryViewModel>> Handle(DeviceCategoryListQuery request, CancellationToken cancellationToken)
        {
            var dataQuery = await UnitOfWork.DeviceCategoryRepository.GetAllAsNoTracking().Select(x => new DeviceCategoryViewModel()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();

            return dataQuery;
        }
    }
}
