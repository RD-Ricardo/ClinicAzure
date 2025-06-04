using ClinicAzure.Shared.Dtos.AzureEntraID;

namespace ClinicAzure.Application.Intefaces
{
    public interface IAzureEntraIDService
    {
        Task<TenantDto> GetTenantAsync();
        Task<List<SignInDto>> GetSignInsCurrentUserAsync(string userId);
        Task<List<GroupDto>> GetGroupsAsync();
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> GetCurerntUserAsync(string userId);
    }
}
