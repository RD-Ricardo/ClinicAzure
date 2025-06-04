using ClinicAzure.Shared.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAzure.Api.Extensions
{
    public static class Errors
    {
        public static IActionResult Get((ErrorType errorType, List<string>? erros) errorProblema)
        {
            var (errorType, erros) = errorProblema;

            var obj = new { Errors = erros ?? [], Type = errorType.ToString() };

            if (errorType == ErrorType.NotFound)
            {
                return new NotFoundObjectResult(obj);
            }
            else if (errorType == ErrorType.Invalid)
            {
                return new BadRequestObjectResult(obj);
            }
            else if (errorType == ErrorType.Conflict)
            {
                return new ConflictObjectResult(obj);
            }
            else if (errorType == ErrorType.InternalServerError)
            {
                return new BadRequestObjectResult(obj);
            }
            else
            {
                return new BadRequestObjectResult(obj);
            }
        }
    }
}
