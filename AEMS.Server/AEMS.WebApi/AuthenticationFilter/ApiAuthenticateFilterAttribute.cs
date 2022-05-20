using AEMS.Utilities;
using AEMS.Utilities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace AEMS.WebApi.AuthenticationFilter
{
    public class ApiAuthenticateFilterAttribute : ActionFilterAttribute
    {
        #region Fields

        /// <summary>
        /// The authentication session
        /// </summary>
        private readonly IAuthSession _authSession;

        #endregion

        #region Contrustor

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiAuthenticateFilterAttribute"/> class.
        /// </summary>
        /// <param name="authSession">The authentication session.</param>
        public ApiAuthenticateFilterAttribute(IAuthSession authSession)
        {
            _authSession = authSession;
        }

        #endregion

        #region OnActionExecuting

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues tokenVal);

            var token = tokenVal.ToString().Split(" ").Last();

            var authSessionModel = JwtHelper.ValidateToken(token);

            _authSession.SetUserSession(authSessionModel);

            base.OnActionExecuting(context);
        }

        #endregion


        #region OnActionExecuted

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                if (context.Result is ObjectResult objectResult)
                {
                    if (objectResult.Value is BaseApiResponseModel result && !result.IsSuccess)
                    {
                        switch (result.StatusCode)
                        {
                            case HttpStatusCodes.Unauthorized:
                                context.Result = new UnauthorizedObjectResult("Permission denied, wrong credentials or user not be allowed access.");
                                break;
                            case HttpStatusCodes.NotFound:
                                context.Result = new NotFoundObjectResult("Data not found.");
                                break;
                            case HttpStatusCodes.Forbidden:
                                context.Result = new StatusCodeResult(HttpStatusCodes.Forbidden);
                                break;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        #endregion

        



    }
}
