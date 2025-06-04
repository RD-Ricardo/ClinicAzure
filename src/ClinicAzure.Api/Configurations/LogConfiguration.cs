using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using Serilog.Enrichers.Span;

namespace ClinicAzure.Api.Configurations
{
    public static class LogConfiguration
    {
        public static IServiceCollection AddLogConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddHttpLogging(o =>
            {
                o.LoggingFields = HttpLoggingFields.RequestPath | HttpLoggingFields.RequestMethod | HttpLoggingFields.RequestQuery | HttpLoggingFields.RequestBody | HttpLoggingFields.Duration | HttpLoggingFields.ResponseStatusCode;
                o.CombineLogs = true;
            });

            services.AddSerilog(opt =>
            {
                opt.MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning);
                opt.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning);
                opt.MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning);
                opt.Enrich.FromLogContext();
                opt.Enrich.WithProperty("Application", "Api");
                opt.WriteTo.Console();
                opt.Enrich.WithSpan(new SpanOptions() { IncludeOperationName = true, IncludeTags = true });
            });

            return services;
        }
    }
}
