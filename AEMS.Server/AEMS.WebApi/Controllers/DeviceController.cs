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
    [ApiVersion(ApiVersions.ApiVersionV1)]
    public class DeviceController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="userAuthSession">The user authentication session.</param>
        public DeviceController(IMediator mediator, IAuthSession userAuthSession) : base(mediator, userAuthSession)
        {
        }


        [HttpGet]
        [ProducesResponseType(typeof(BaseApiResponseModel), HttpStatusCodes.Ok)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Unauthorized)]
        [ServiceFilter(typeof(ApiAuthenticateFilterAttribute))]
        public async Task<BaseApiResponseModel> Get()
        {
            var result = await Mediator.Send(new GetDeviceListQuery());
            return BaseApiResponse.OK(result, result.Count());
        }

        [HttpGet]
        [ProducesResponseType(typeof(BaseApiResponseModel), HttpStatusCodes.Ok)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Unauthorized)]
        [Route("detail")]
        [ServiceFilter(typeof(ApiAuthenticateFilterAttribute))]
        public async Task<BaseApiResponseModel> GetDetail([FromQuery] DeviceDetailQuery model)
        {
            var result = await Mediator.Send(model);
            return BaseApiResponse.OK(result);
        }


        [HttpPut]
        [ProducesResponseType(typeof(BaseApiResponseModel), HttpStatusCodes.Ok)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Unauthorized)]
        [ServiceFilter(typeof(ApiAuthenticateFilterAttribute))]
        public async Task<BaseApiResponseModel> Update([FromBody] UpdateDeviceCommand model)
        {
            return await Mediator.Send(model);
        }
    }
}
