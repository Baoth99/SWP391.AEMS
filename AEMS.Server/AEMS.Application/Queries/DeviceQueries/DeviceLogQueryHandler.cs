using AEMS.Data.EF.UnitOfWork;
using AEMS.MSAzureService;
using AEMS.ORM.Dapper;
using AEMS.Utilities;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AEMS.Application.Queries.DeviceQueries
{
    public class DeviceLogQueryHandler : BaseDataAccess, IQueryHandler<DeviceLogQuery, IEnumerable<DeviceLogViewModel>>
    {
        private readonly ICosmosDBService _cosmosDBService;


        public DeviceLogQueryHandler(IUnitOfWork unitOfWork, IDapperService dapperService, IAuthSession authSession, TelemetryClient telemetryClient,
                                    ICosmosDBService cosmosDBService) : base(unitOfWork, dapperService, authSession, telemetryClient)
        {
            _cosmosDBService = cosmosDBService;
        }

        public async Task<IEnumerable<DeviceLogViewModel>> Handle(DeviceLogQuery request, CancellationToken cancellationToken)
        {
            var isDeviceExisted = await UnitOfWork.DeviceRepository.IsExistedAsync(x => x.Code == request.DeviceCode);
            if (!isDeviceExisted)
            {
                return null;
            }

            _cosmosDBService.SetCosmosDbInfo(AppSettingValues.CosmosDbAemsDb, AppSettingValues.CosmosDbDeviceLogContainer);

            var query = _cosmosDBService.GetMany<DeviceLogViewModel>(x => x.DeviceId == request.DeviceCode);
            return query;
        }
    }
}
