using AEMS.Data.EF.UnitOfWork;
using AEMS.DataAccess.DTOs;
using AEMS.DataAccess.Models;
using AEMS.ORM.Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AEMS.DataAccess.Queries
{
    public class DeviceQueryHandler : BaseDataAccess, IQueryHandler<GetDeviceListQuery, IEnumerable<DeviceViewModel>>
    {
        public DeviceQueryHandler(IUnitOfWork unitOfWork, IDapperService dapperService) : base(unitOfWork, dapperService)
        {

        }

        public async Task<IEnumerable<DeviceViewModel>> Handle(GetDeviceListQuery request, CancellationToken cancellationToken)
        {
            
            var dataQuery = UnitOfWork.DeviceRepository.GetManyAsNoTracking(x => string.IsNullOrEmpty(request.Code) || x.Code.Contains(request.Code) &&
                                                                               string.IsNullOrEmpty(request.Name) || x.Name.Contains(request.Name))
                                                     .Join(UnitOfWork.DeviceCategoryRepository.GetManyAsNoTracking(x => request.DeviceCategoryId == null || x.Id == request.DeviceCategoryId), 
                                                                                                                                        x => x.DeviceCategoryId, y => y.Id, (x, y) => new
                                                                                                                                        {
                                                                                                                                            Device = x
                                                                                                                                        })
                                                     .Join(UnitOfWork.AreaRepository.GetManyAsNoTracking(x => request.AreaId == null || x.Id == request.AreaId),
                                                                                                                                        x => x.Device.AreaId, y => y.Id, (x, y) => new
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

            var result = await dataQuery.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).Select(x => new DeviceViewModel()
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
