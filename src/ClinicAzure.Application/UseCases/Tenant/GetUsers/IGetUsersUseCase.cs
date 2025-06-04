using ClinicAzure.Shared.Abstractions;
using ClinicAzure.Shared.Dtos.AzureEntraID;

namespace ClinicAzure.Application.UseCases.Tenant.GetUsers
{
    public interface IGetUsersUseCase : IUseCase
    {
        Task<Result<List<UserDto>>> ExecuteAsync(CancellationToken cancellationToken);
    }
}
