using ClinicAzure.Application.Intefaces;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ClinicAzure.Api.Filters
{
 
    public class AuthenticationFilter : IAsyncActionFilter
    {
        private readonly IApplicationUser _applicationUser;

        const string ObjectIdentifierClaim = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        const string NameClaim = "name";
        const string TenantIdClaim = "http://schemas.microsoft.com/identity/claims/tenantid";
        public AuthenticationFilter(IApplicationUser applicationUser)
        {
            _applicationUser = applicationUser;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var endpoint = context.ActionDescriptor as ControllerActionDescriptor;

            var hasAuthorize = endpoint?.MethodInfo.GetCustomAttributes(typeof(Microsoft.AspNetCore.Authorization.AuthorizeAttribute), true).Any() == true
                || endpoint?.ControllerTypeInfo.GetCustomAttributes(typeof(Microsoft.AspNetCore.Authorization.AuthorizeAttribute), true).Any() == true;

            if (hasAuthorize)
            {
                var user = context.HttpContext.User;
                if (user.Identity?.IsAuthenticated == true)
                {
                    _applicationUser.Id = user.FindFirstValue(ObjectIdentifierClaim) ?? string.Empty;
                    _applicationUser.UserName = user.FindFirstValue(NameClaim) ?? string.Empty;
                    _applicationUser.TenantId = user.FindFirstValue(TenantIdClaim) ?? string.Empty;
                }
            }

            await next();
        }
    }
}
