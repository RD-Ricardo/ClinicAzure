using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClinicAzure.Api.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var watch = Stopwatch.StartNew();

                context.Request.EnableBuffering();

                string requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
                context.Request.Body.Position = 0;

                await _next(context);

                watch.Stop();

                if (context.Request.Method != "OPTIONS")
                {
                    if (context.Request.Path.ToString().Contains("auth"))
                    {
                        var body = JsonConvert.DeserializeObject<JObject>(requestBody);

                        if (body != null)
                        {
                            body["password"] = "********";
                        }

                        requestBody = JsonConvert.SerializeObject(body);
                    }

                    _logger.LogInformation("{LogEvent} {Ip} {Method} {Path} {ResponseTime} {StatusCode} {UserId} {UserName} {query} {requestBody}",
                        "HttpRequest",
                        context.Connection.RemoteIpAddress?.MapToIPv4(),
                        context.Request.Method,
                        context.Request.Path,
                        watch.ElapsedMilliseconds,
                        context.Response.StatusCode,
                        context.User?.FindFirstValue("id"),
                        context.User?.FindFirstValue("name"),
                        context.Request.QueryString,
                        requestBody
                     );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
