using AEMS.Application;
using AEMS.DataAccess.DTOs;
using AEMS.MSAzureService;
using AEMS.Utilities;
using AEMS.Utilities.BaseResponse;
using AEMS.WebApi.AuthenticationFilter;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AEMS.WebApi.Controllers
{
    [ApiVersion(ApiVersions.ApiVersionV1)]
    public class DeviceController : BaseController
    {
        /// <summary>
        /// The equipment service
        /// </summary>
        private readonly IDeviceService _deviceService;

        private readonly TelemetryClient _telemetryClient;


        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceController"/> class.
        /// </summary>
        /// <param name="deviceService">The device service.</param>
        /// <param name="telemetryClient">The telemetry client.</param>
        public DeviceController(IDeviceService deviceService, TelemetryClient telemetryClient)
        {
            _deviceService = deviceService;
            _telemetryClient = telemetryClient;
        }


        [HttpGet]
        [ProducesResponseType(typeof(BaseApiResponseModel), HttpStatusCodes.Ok)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Unauthorized)]
        [Route("devices")]
        [ServiceFilter(typeof(ApiAuthenticateFilterAttribute))]
        public async Task<BaseApiResponseModel> Get([FromQuery] GetDeviceListQuery model)
        {
            return await _deviceService.GetDevices(model);
        }
    }
}
