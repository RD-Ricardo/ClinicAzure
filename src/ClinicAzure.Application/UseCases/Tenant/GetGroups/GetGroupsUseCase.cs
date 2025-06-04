using ClinicAzure.Application.Intefaces;
using ClinicAzure.Shared.Abstractions;
using ClinicAzure.Shared.Dtos.AzureEntraID;

namespace ClinicAzure.Application.UseCases.Tenant.GetGroups
{
    public class GetGroupsUseCase : IGetGroupsUseCase
    {
        private readonly IAzureEntraIDService _azureEntraIDService;
        public GetGroupsUseCase(IAzureEntraIDService azureEntraIDService)
        {
            _azureEntraIDService = azureEntraIDService;
        }

        public async Task<Result<List<GroupDto>>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var groups = await _azureEntraIDService.GetGroupsAsync();

            return Result<List<GroupDto>>.Ok(groups);
        }
    }
}
