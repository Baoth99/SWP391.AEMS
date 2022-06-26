using AEMS.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEMS.WebApi.Controllers
{
    [Route(ApplicationRestfulApi.BaseApiUrl)]
    [Produces(ApplicationRestfulApi.ApplicationProduce)]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected IMediator Mediator { get; private set; }

        protected IAuthSession UserAuthSession { get; private set; }

        public BaseController(IMediator mediator, IAuthSession userAuthSession)
        {
            Mediator = mediator;
            UserAuthSession = userAuthSession;
        }
    }
}
