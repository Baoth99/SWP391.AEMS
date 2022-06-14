using AEMS.DataAccess.DTOs;
using AEMS.Utilities;
using AEMS.Utilities.BaseResponse;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace AEMS.Application
{
    public class DeviceService : BaseService, IDeviceService
    {
        public DeviceService(IMediator mediator) : base(mediator)
        {
        }

        public async Task<BaseApiResponseModel> GetDevices(GetDeviceListQuery model)
        {
            var res = await Mediator.Send(model);
            return BaseApiResponse.OK(res, res.Count());
        }
    }
}
