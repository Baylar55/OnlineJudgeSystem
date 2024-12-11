using Microsoft.AspNetCore.Mvc.Filters;

namespace AlgoCode.Application.Common.Attributes
{
    public class OnlyAnonymousAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context?.HttpContext?.User?.Identity?.IsAuthenticated == true)
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
