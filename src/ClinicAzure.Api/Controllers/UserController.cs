using ClinicAzure.Api.Extensions;
using ClinicAzure.Application.UseCases.User.GetSignIns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAzure.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        [HttpGet("get-sign-ins")]
        public async Task<IActionResult> GetSignIns([FromServices] IGetSignInsUserUseCase getSignInsUserUseCase, CancellationToken cancellationToken)
        {
            var result = await getSignInsUserUseCase.ExecuteAsync(cancellationToken);

            return result.Match(
                r => Ok(r),
                e => Errors.Get(e)
            );
        }
    }
}
