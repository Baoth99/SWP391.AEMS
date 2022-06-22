using AEMS.ORM.Dapper;
using AEMS.Data.EF.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AEMS.Utilities;
using Microsoft.ApplicationInsights;

namespace AEMS.Application.Queries
{
    public class DeviceListQueryHandler : BaseDataAccess, IQueryHandler<GetDeviceListQuery, IEnumerable<DeviceViewModel>>
    {
        public DeviceListQueryHandler(IUnitOfWork unitOfWork,IDapperService dapperService, 
                                  IAuthSession authSession, TelemetryClient telemetryClient) : base(unitOfWork, dapperService, authSession, telemetryClient)
        {
        }

        public async Task<IEnumerable<DeviceViewModel>> Handle(GetDeviceListQuery request, CancellationToken cancellationToken)
        {

            var dataQuery = UnitOfWork.DeviceRepository.GetAllAsNoTracking().Join(UnitOfWork.DeviceCategoryRepository.GetAllAsNoTracking(),x => x.DeviceCategoryId, y => y.Id, (x, y) => new
                                                                                                                                        {
                                                                                                                                            Device = x
                                                                                                                                        })
                                                                            .Join(UnitOfWork.AreaRepository.GetAllAsNoTracking(),x => x.Device.AreaId, y => y.Id, (x, y) => new
                                                                                                                                        {
                                                                                                                                            EquipmentId = x.Device.Id,
                                                                                                                                            Code = x.Device.Code,
                                                                                                                                            Name = x.Device.Name,
                                                                                                                                            Imei = x.Device.Imei,
                                                                                                                                            BusinessVersion = x.Device.BusinessVersion,
                                                                                                                                            FirmwareVersion = x.Device.FirmwareVersion,
                                                                                                                                            ManagementVersion = x.Device.ManagementVersion,
                                                                                                                                            Description = x.Device.Description,
                                                                                                                                            Status = x.Device.Status,
                                                                                                                                            x.Device.CreatedAt
                                                                                                                                        })
                                                                            .OrderByDescending(x => x.CreatedAt);

            var result = await dataQuery.Select(x => new DeviceViewModel()
            {
                Id = x.EquipmentId,
                Code = x.Code,
                Name = x.Name,
                Imei = x.Imei,
                ManagementVersion = x.ManagementVersion,
                BusinessVersion = x.BusinessVersion,
                Description = x.Description,
                FirmwareVersion = x.FirmwareVersion,
                Status = x.Status
            }).ToListAsync();

            return result;
        }
    }
}
