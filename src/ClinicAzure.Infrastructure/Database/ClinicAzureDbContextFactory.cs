using ClinicAzure.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ClinicAzure.Infrastructure.Database
{
    public class ClinicAzureDbContextFactory : IDesignTimeDbContextFactory<ClinicAzureDbContext>
    {
        public ClinicAzureDbContext CreateDbContext(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{env}.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ClinicAzureDbContext>()
                .UseNpgsql(configuration["Postgres"]);

            return new ClinicAzureDbContext(builder.Options, new ApplicationUser());
        }
    }
}
