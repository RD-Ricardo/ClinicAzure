using System.Reactive;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;
using ClinicAzure.Shared.Abstractions;
using ClinicAzure.Shared.Abstractions.Errors;

namespace ClinicAzure.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private string ErrorMessage = "Erro Interno. Tente novamente mais tarde.";
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";
            
            ErrorMessage = exception.Message;

            var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();

            if (contextFeature != null)
            {
                if (contextFeature.Error is DbUpdateException)
                {
                    await HandleDbUpdateException(httpContext, (DbUpdateException)contextFeature.Error);
                    return true;
                }

                httpContext.Response.StatusCode = HandleUhandledException(exception);

                var result = Result<bool>.Fail(new InternalError(ErrorMessage));

                var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                    Converters = { new Newtonsoft.Json.Converters.StringEnumConverter() }
                });

                await httpContext.Response.WriteAsync(json);
            }
            return true;
        }

        private async Task HandleDbUpdateException(HttpContext httpContext, DbUpdateException ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
            {
                ErrorMessage = $"Este registro já existe. Restrição: {pgEx.ConstraintName}";
                ErrorMessage += string.IsNullOrEmpty(pgEx.ColumnName) ? "." : $"Coluna: {pgEx.ColumnName}";

                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            }

            var result = Result<bool>.Fail(new InternalError(ErrorMessage));

            var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                Converters = { new Newtonsoft.Json.Converters.StringEnumConverter() }
            });

            await httpContext.Response.WriteAsync(json);
        }

        private int HandleUhandledException(Exception ex)
        {
            ErrorMessage = $"Erro interno. {ex.Message}.";
            return StatusCodes.Status500InternalServerError;
        }
    }
}
