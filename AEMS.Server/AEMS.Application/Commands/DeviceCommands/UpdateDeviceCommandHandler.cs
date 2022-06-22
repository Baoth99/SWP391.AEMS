using AEMS.Data.EF.UnitOfWork;
using AEMS.Data.Entities;
using AEMS.ORM.Dapper;
using AEMS.Utilities;
using AEMS.Utilities.BaseResponse;
using Microsoft.ApplicationInsights;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AEMS.Application.Commands.DeviceCommands
{
    public class UpdateDeviceCommandHandler : BaseDataAccess, ICommandHandler<UpdateDeviceCommand, BaseApiResponseModel>
    {
        public UpdateDeviceCommandHandler(IUnitOfWork unitOfWork, IDapperService dapperService, IAuthSession authSession, TelemetryClient telemetryClient) : base(unitOfWork, dapperService, authSession, telemetryClient)
        {
        }

        public async Task<BaseApiResponseModel> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var entity = UnitOfWork.DeviceRepository.GetById(request.Id);
            if (entity == null)
            {
                return BaseApiResponse.NotFound();
            }

            entity.Name = request.Name;
            entity.Description = request.Description;

            if (request.Photos.Any())
            {
                var photos = UnitOfWork.PhotoRepository.GetManyAsNoTracking(x => x.RecorderId == request.Id).AsEnumerable().Select(x =>
                {
                    x.IsUsed = false;
                    return x;
                });

                UnitOfWork.PhotoRepository.UpdateRange(photos.ToList());


                var updatedPhoto = request.Photos.Select(x => new Photo()
                {
                    RecorderId = request.Id,
                    IsUsed = true,
                    Url = x
                }).ToList();

                UnitOfWork.PhotoRepository.InsertRange(photos.ToList());
            }

            await UnitOfWork.CommitAsync();

            return BaseApiResponse.OK();
        }
    }
}
