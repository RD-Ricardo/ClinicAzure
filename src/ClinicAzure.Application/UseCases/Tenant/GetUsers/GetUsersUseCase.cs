using ClinicAzure.Application.Intefaces;
using ClinicAzure.Shared.Abstractions;
using ClinicAzure.Shared.Dtos.AzureEntraID;

namespace ClinicAzure.Application.UseCases.Tenant.GetUsers
{
    public class GetUsersUseCase : IGetUsersUseCase
    {
        private readonly IAzureEntraIDService _azureEntraIDService;
        public GetUsersUseCase(IAzureEntraIDService azureEntraIDService)
        {
            _azureEntraIDService = azureEntraIDService;
        }

        public async Task<Result<List<UserDto>>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var users = await _azureEntraIDService.GetUsersAsync();

            return Result<List<UserDto>>.Ok(users);
        }
    }
}
