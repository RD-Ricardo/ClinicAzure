using ClinicAzure.Application.Intefaces;
using ClinicAzure.Shared.Abstractions;
using ClinicAzure.Shared.Dtos.AzureEntraID;

namespace ClinicAzure.Application.UseCases.User.GetSignIns
{
    public class GetSignInsUserUseCase : IGetSignInsUserUseCase
    {
        private readonly IAzureEntraIDService _azureEntraIDService;

        private readonly IApplicationUser _applicationUser;
        public GetSignInsUserUseCase(IAzureEntraIDService azureEntraIDService, IApplicationUser applicationUser)
        {
            _azureEntraIDService = azureEntraIDService;
            _applicationUser = applicationUser;
        }

        public async Task<Result<List<SignInDto>>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var signIns = await _azureEntraIDService.GetSignInsCurrentUserAsync(_applicationUser.Id);

            return Result<List<SignInDto>>.Ok(signIns);
        }
    }
}
