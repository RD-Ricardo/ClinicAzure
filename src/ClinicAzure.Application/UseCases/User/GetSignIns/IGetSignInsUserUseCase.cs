using ClinicAzure.Shared.Abstractions;
using ClinicAzure.Shared.Dtos.AzureEntraID;

namespace ClinicAzure.Application.UseCases.User.GetSignIns
{
    public interface IGetSignInsUserUseCase : IUseCase
    {
        Task<Result<List<SignInDto>>> ExecuteAsync(CancellationToken cancellationToken);
    }
}
