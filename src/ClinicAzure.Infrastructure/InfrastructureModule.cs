using ClinicAzure.Shared.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ClinicAzure.Infrastructure.Settings;
using ClinicAzure.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Microsoft.Graph;
using ClinicAzure.Application.Intefaces;
using ClinicAzure.Infrastructure.Services;

namespace ClinicAzure.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClinicAzureDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Postgres"));
            });

            services.Configure<JwtSettings>(options => configuration.GetSection("JwtSettings").Bind(options));
            services.Configure<AzureEntraIDSettings>(options => configuration.GetSection("AzureAdSettings").Bind(options));

            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var tenantId = config["AzureAdSettings:TenantId"];
                var clientId = config["AzureAdSettings:ClientId"];
                var clientSecret = config["AzureAdSettings:Secret"];
                var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                return new GraphServiceClient(credential,
                    scopes: [
                        "https://graph.microsoft.com/.default"
                    ]);
            });

            services.AddScoped<IAzureEntraIDService, AzureEntraIDService>();
            services.AddScoped<IApplicationUser, ApplicationUser>();

            services.Scan(scan => scan
                .FromAssemblies(typeof(InfrastructureModule).Assembly)
                .AddClasses(classes => classes.AssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            return services;
        }
    }
}
