using Microsoft.Extensions.DependencyInjection;
using ClinicAzure.Shared.Abstractions;

namespace ClinicAzure.Application
{
    public static class ApplicationModule
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var currentAssembly = typeof(ApplicationModule).Assembly;

            services.Scan(scan => scan
                .FromAssemblies(currentAssembly)
                .AddClasses(classes => classes.AssignableTo<IUseCase>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
        }
    }
}
