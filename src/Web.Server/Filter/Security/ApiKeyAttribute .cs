using Microsoft.AspNetCore.Mvc;

namespace Web.Server.Filter.Security
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute()
            : base(typeof(ApiKeyAuthorizationFilter))
        {
        }
    }
}
