using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Web.Domain;

namespace Web.Server.Filter.Security
{
    public class ApiKeyAuthorizationFilter : IAuthorizationFilter
    {
        private const string ApiKeyHeaderName = "X-API-Key";

        private readonly IApiKeyValidator _apiKeyValidator;

        public ApiKeyAuthorizationFilter(IApiKeyValidator apiKeyValidator)
        {
            _apiKeyValidator = apiKeyValidator;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];

            if (!_apiKeyValidator.IsValid(apiKey))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult(Error.Response(901).TryCast<CommonResponse>());
            }
        }
    }
}
