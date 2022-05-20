using AEMS.DataAccess.DTOs;
using AEMS.Utilities;
using AEMS.Utilities.BaseResponse;
using MediatR;
using System.Threading.Tasks;

namespace AEMS.Application
{
    public class EquipmentService : BaseService, IEquipmentService
    {
        public EquipmentService(IMediator mediator) : base(mediator)
        {
        }

        public async Task<BaseApiResponseModel> GetList(GetEquipmentListQuery model)
        {
            var res = await Mediator.Send(model);
            return BaseApiResponse.OK(res);
        }
    }
}
