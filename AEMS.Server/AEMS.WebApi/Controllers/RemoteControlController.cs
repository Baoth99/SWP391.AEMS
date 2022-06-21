using AEMS.AWSService;
using AEMS.Utilities;
using AEMS.Utilities.BaseResponse;
using AEMS.WebApi.AuthenticationFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AEMS.WebApi.Controllers
{
    [ApiVersion(ApiVersions.ApiVersionV1)]
    public class RemoteControlController : BaseController
    {
        private readonly IAWSIotCoreService _AWSIotCoreService;

        public RemoteControlController(IMediator mediator, IAuthSession userAuthSession, IAWSIotCoreService AWSIotCoreService) : base(mediator, userAuthSession)
        {
            _AWSIotCoreService = AWSIotCoreService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseApiResponseModel), HttpStatusCodes.Ok)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Unauthorized)]
        [ServiceFilter(typeof(ApiAuthenticateFilterAttribute))]
        public async Task<BaseApiResponseModel> Get([FromBody] TelemetryDeviceSetting model)
        {
            await _AWSIotCoreService.PublishMQTTMessage(AppSettingValues.AWSTelemetrySettingDeviceTopic, model);
            return BaseApiResponse.OK();
        }

    }
}
