using AEMS.Application;
using AEMS.DataAccess.DTOs;
using AEMS.Utilities;
using AEMS.WebApi.AuthenticationFilter;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AEMS.WebApi.Controllers
{
    [ApiVersion(ApiVersions.ApiVersionV1)]
    public class EquipmentController : BaseController
    {
        /// <summary>
        /// The equipment service
        /// </summary>
        private readonly IEquipmentService _equipmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentController"/> class.
        /// </summary>
        /// <param name="equipmentService">The equipment service.</param>
        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BaseApiResponseModel), HttpStatusCodes.Ok)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseModel), HttpStatusCodes.Unauthorized)]
        [Route("equipments")]
        [ServiceFilter(typeof(ApiAuthenticateFilterAttribute))]
        public async Task<BaseApiResponseModel> Get([FromQuery] GetEquipmentListQuery model)
        {
            return await _equipmentService.GetList(model);
        }
    }
}
