using ClinicAzure.Application.Intefaces;
using ClinicAzure.Shared.Abstractions;
using ClinicAzure.Shared.Abstractions.Errors;
using ClinicAzure.Shared.Dtos.AzureEntraID;

namespace ClinicAzure.Application.UseCases.Tenant.GetTenant
{
    public class GetTenantUseCase : IGetTenantUseCase
    {
        private readonly IAzureEntraIDService _azureEntraIDService;
        public GetTenantUseCase(IAzureEntraIDService azureEntraIDService)
        {
            _azureEntraIDService = azureEntraIDService;
        }

        public async Task<Result<TenantDto>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var tenant = await _azureEntraIDService.GetTenantAsync();

            if (tenant is null) 
            {
                return Result<TenantDto>.Fail(new NotFoundError("Organização não encontrada")); ;
            }

            return Result<TenantDto>.Ok(tenant);
        }
    }
}
