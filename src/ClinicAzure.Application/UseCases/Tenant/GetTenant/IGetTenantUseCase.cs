using ClinicAzure.Shared.Abstractions;
using ClinicAzure.Shared.Dtos.AzureEntraID;

namespace ClinicAzure.Application.UseCases.Tenant.GetTenant
{
    public interface IGetTenantUseCase : IUseCase
    {
        Task<Result<TenantDto>> ExecuteAsync(CancellationToken cancellationToken);
    }
}
