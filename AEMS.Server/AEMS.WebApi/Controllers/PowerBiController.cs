using AEMS.MSAzureService;
using AEMS.Utilities;
using AEMS.Utilities.BaseResponse;
using AEMS.WebApi.AuthenticationFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AEMS.WebApi.Controllers
{
    [ApiVersion(ApiVersions.ApiVersionV1)]
    public class PowerBiController : BaseController
    {
        private readonly IPowerBIService _powerBIService;

        public PowerBiController(IMediator mediator, IAuthSession userAuthSession,
                                IPowerBIService powerBIService) : base(mediator, userAuthSession)
        {
            _powerBIService = powerBIService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BaseApiResponseModel), HttpStatusCodes.Ok)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Unauthorized)]
        [Route("customer-info")]
        [ServiceFilter(typeof(ApiAuthenticateFilterAttribute))]
        public async Task<BaseApiResponseModel> CustomerInfoReport()
        {
            var result = await _powerBIService.GetReportToken(AppSettingValues.PowerBICustomerInfoReport);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                return BaseApiResponse.Error(SystemMessageCode.ServiceException, result.ErrorMessage);
            }

            return BaseApiResponse.OK(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(BaseApiResponseModel), HttpStatusCodes.Ok)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Unauthorized)]
        [Route("device")]
        [ServiceFilter(typeof(ApiAuthenticateFilterAttribute))]
        public async Task<BaseApiResponseModel> DeviceReport()
        {
            var result = await _powerBIService.GetReportToken(AppSettingValues.PowerBIDeviceReport);
            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                return BaseApiResponse.Error(SystemMessageCode.ServiceException, result.ErrorMessage);
            }

            return BaseApiResponse.OK(result);
        }
    }
}
