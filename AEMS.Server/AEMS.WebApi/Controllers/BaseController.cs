using AEMS.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEMS.WebApi.Controllers
{
    [Route(ApplicationRestfulApi.BaseApiUrl)]
    [Produces(ApplicationRestfulApi.ApplicationProduce)]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [Authorize]
    public class BaseController
    {
    }
}
