using ClinicAzure.Shared.Abstractions;
using ClinicAzure.Shared.Dtos.AzureEntraID;

namespace ClinicAzure.Application.UseCases.Tenant.GetGroups
{
    public interface IGetGroupsUseCase : IUseCase
    {
        Task<Result<List<GroupDto>>> ExecuteAsync(CancellationToken cancellationToken);
    }
}
