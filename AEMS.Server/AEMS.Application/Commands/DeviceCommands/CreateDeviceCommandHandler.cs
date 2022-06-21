using AEMS.Data.EF.UnitOfWork;
using AEMS.Data.Entities;
using AEMS.ORM.Dapper;
using AEMS.Utilities;
using AEMS.Utilities.BaseResponse;
using MediatR;
using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AEMS.Application.Commands.DeviceCommands
{
    public class CreateDeviceCommandHandler : BaseDataAccess, ICommandHandler<CreateDeviceCommand, BaseApiResponseModel>
    {

        public CreateDeviceCommandHandler(IUnitOfWork unitOfWork, IDapperService dapperService, IAuthSession authSession, TelemetryClient telemetryClient) : base(unitOfWork, dapperService, authSession, telemetryClient)
        {
        }

        public async Task<BaseApiResponseModel> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            //Validate

            var entity = new Device()
            {
                Code = request.Code,
                Name = request.Name,
                Imei = request.Imei,
                BusinessVersion = request.BusinessVersion,
                FirmwareVersion = request.FirmwareVersion,
                ManagementVersion = request.ManagementVersion,
                Status = request.Status,
                DeviceCategoryId = request.DeviceCategoryId,
                AreaId = request.AreaId,
            };

            UnitOfWork.DeviceRepository.Insert(entity);

            await UnitOfWork.CommitAsync();




            return BaseApiResponse.OK();
        }
    }
}
