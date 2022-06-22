using AEMS.Application;
using AEMS.Utilities;
using AEMS.Utilities.BaseResponse;
using AEMS.WebApi.AuthenticationFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AEMS.WebApi.Controllers
{
    public class DeviceCategoryController : BaseController
    {
        public DeviceCategoryController(IMediator mediator, IAuthSession userAuthSession) : base(mediator, userAuthSession)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(BaseApiResponseModel), HttpStatusCodes.Ok)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Unauthorized)]
        [ServiceFilter(typeof(ApiAuthenticateFilterAttribute))]
        public async Task<BaseApiResponseModel> Get()
        {
            var result = await Mediator.Send(new DeviceCategoryListQuery());
            return BaseApiResponse.OK(result, result.Count());
        }
    }
}
