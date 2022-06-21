using AEMS.AWSService;
using AEMS.Data.EF.UnitOfWork;
using AEMS.MSAzureService;
using AEMS.ORM.Dapper;
using AEMS.Utilities;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AEMS.Application.Queries.DeviceQueries
{
    public class DeviceDetailQueryHandler : BaseDataAccess, IQueryHandler<DeviceDetailQuery, DeviceDetailViewModel>
    {
        private readonly IPowerBIService _powerBIService;

        private readonly IAWSS3Service _AWSS3Service;

        public DeviceDetailQueryHandler(IUnitOfWork unitOfWork, IDapperService dapperService, 
                                        IAuthSession authSession, TelemetryClient telemetryClient,
                                        IPowerBIService powerBIService, IAWSS3Service AWSS3Service) : base(unitOfWork, dapperService, authSession, telemetryClient)
        {
            _powerBIService = powerBIService;
            _AWSS3Service = AWSS3Service;
        }

        public async Task<DeviceDetailViewModel> Handle(DeviceDetailQuery request, CancellationToken cancellationToken)
        {
            var isExisted = await UnitOfWork.DeviceRepository.IsExistedAsync(x => x.Id == request.Id);
            if (!isExisted)
            {
                return default;
            }

            var dataQuery = UnitOfWork.DeviceRepository.GetManyAsNoTracking(x => x.Id == request.Id)
                                 .Join(UnitOfWork.DeviceCategoryRepository.GetAllAsNoTracking(), x => x.DeviceCategoryId, y => y.Id, (x, y) => new
                                 {
                                     x.Id,
                                     x.Code,
                                     x.Name,
                                     x.Status,
                                     x.Imei,
                                     x.BusinessVersion,
                                     x.FirmwareVersion,
                                     x.ManagementVersion,
                                     x.Description,
                                     x.PowerBiDashboardId,
                                     x.AreaId,
                                     CategoryName = y.Name,
                                     CategoryCode = y.Code
                                 })
                                 .Join(UnitOfWork.AreaRepository.GetAllAsNoTracking(), x => x.AreaId, y => y.Id, (x, y) => new
                                 {
                                     x.Id,
                                     x.Code,
                                     x.Name,
                                     x.Status,
                                     x.Imei,
                                     x.BusinessVersion,
                                     x.FirmwareVersion,
                                     x.ManagementVersion,
                                     x.Description,
                                     x.PowerBiDashboardId,
                                     x.AreaId,
                                     x.CategoryName,
                                     x.CategoryCode,
                                     AreaName = y.Name,
                                     AreaCode = y.Code,
                                     Region = y.Region
                                 }).FirstOrDefault();

            var powerBiToken = new PBITokenModel();

            if (dataQuery.PowerBiDashboardId != null)
            {
                var tokenInfo = await _powerBIService.GetDashboardToken(dataQuery.PowerBiDashboardId);

                if (string.IsNullOrEmpty(tokenInfo.ErrorMessage))
                {
                    powerBiToken.EmmbedToken = tokenInfo.EmmbedToken;
                    powerBiToken.EmmbedUrl = tokenInfo.EmmbedUrl;
                    powerBiToken.Type = tokenInfo.Type;
                    powerBiToken.Id = tokenInfo.Id;
                }
                else
                {
                   powerBiToken.ErrorMessage = tokenInfo.ErrorMessage;
                }
            }

            var photos = UnitOfWork.PhotoRepository.GetManyAsNoTracking(x => x.RecorderId == dataQuery.Id);
            var photoUrl = new List<string>();
            if (photos.Any())
            {
                photoUrl = _AWSS3Service.GetMutiApiGatewayS3InvokeURL(photos.Select(x => x.Url).ToList()).ToList();
            }

            return new DeviceDetailViewModel()
            {
                Id = dataQuery.Id,
                Code = dataQuery.Code,
                Name = dataQuery.Name,
                BusinessVersion = dataQuery.BusinessVersion,
                FirmwareVersion = dataQuery.FirmwareVersion,
                ManagementVersion = dataQuery.ManagementVersion,
                Imei = dataQuery.Imei,
                Description = dataQuery.Description,
                AreaName = dataQuery.AreaName,
                AreaCode = dataQuery.AreaCode,
                CategoryCode = dataQuery.CategoryCode,
                CategoryName = dataQuery.CategoryName,
                Region = dataQuery.Region,
                PowerBiEmbed = powerBiToken,
                Photos = photoUrl
            };
        }
    }
}
