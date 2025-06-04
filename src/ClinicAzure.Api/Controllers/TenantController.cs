using ClinicAzure.Api.Extensions;
using ClinicAzure.Application.UseCases.Tenant.GetGroups;
using ClinicAzure.Application.UseCases.Tenant.GetTenant;
using ClinicAzure.Application.UseCases.Tenant.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAzure.Api.Controllers
{
    [ApiController]
    [Route("api/tenants")]
    [Authorize]
    public class TenantController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTenant([FromServices] IGetTenantUseCase getTenantUseCase, CancellationToken cancellationToken)
        {
            var result = await getTenantUseCase.ExecuteAsync(cancellationToken);

            return result.Match(
                r => Ok(r),
                e => Errors.Get(e)
            );
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetGroups([FromServices] IGetGroupsUseCase getGroupsUseCase, CancellationToken cancellationToken)
        {
            var result = await getGroupsUseCase.ExecuteAsync(cancellationToken);

            return result.Match(
                r => Ok(r),
                e => Errors.Get(e)
            );
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetGroups([FromServices] IGetUsersUseCase getUsersUseCase, CancellationToken cancellationToken)
        {
            var result = await getUsersUseCase.ExecuteAsync(cancellationToken);

            return result.Match(
                r => Ok(r),
                e => Errors.Get(e)
            );
        }
    }
}
