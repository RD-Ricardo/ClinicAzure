using Microsoft.Graph;
using ClinicAzure.Application.Intefaces;
using ClinicAzure.Shared.Dtos.AzureEntraID;
using Microsoft.Extensions.Options;
using ClinicAzure.Infrastructure.Settings;

namespace ClinicAzure.Infrastructure.Services
{
    public class AzureEntraIDService : IAzureEntraIDService
    {
        private readonly GraphServiceClient _graphServiceClient;
        public AzureEntraIDService(GraphServiceClient graphServiceClient, IOptions<AzureEntraIDSettings> optionsAzureEntraIDSetting)
        {
            _graphServiceClient = graphServiceClient;
        }

        public async Task<UserDto> GetCurerntUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("userId não pode ser nulo ou vazio.", nameof(userId));

            var user = await _graphServiceClient.Users[userId].GetAsync() ??
                throw new Exception("ERROR-01");

            return new UserDto(user.Id, user.DisplayName, user.UserPrincipalName, user.CreatedDateTime);
        }

        public async Task<List<GroupDto>> GetGroupsAsync()
        {
            var groups = await _graphServiceClient.Groups.GetAsync();
            
            if (groups?.Value == null)
                return [];

            return [.. groups.Value.Select(g => new GroupDto(g.Id, g.DisplayName, g.CreatedDateTime))];
        }

        public async Task<List<SignInDto>> GetSignInsCurrentUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("userId não pode ser nulo ou vazio.", nameof(userId));

            var signIns = await _graphServiceClient.AuditLogs.SignIns
                .GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Filter = $"userId eq '{userId}'";
                    requestConfiguration.QueryParameters.Top = 20;
                });

            if (signIns?.Value == null)
                return [];

            return signIns.Value.Select(s => new SignInDto(s.UserDisplayName, s.CreatedDateTime, s.Status!.ErrorCode)).ToList();
        }

        public async Task<TenantDto> GetTenantAsync()
        {
            var orgs = await _graphServiceClient.Organization.GetAsync();

            var org = orgs?.Value?.FirstOrDefault() ?? 
                throw new Exception("ERROR-02");
            
            return new TenantDto(org.Id!, org.DisplayName!);
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _graphServiceClient.Users.GetAsync();

            if (users?.Value == null)
                return [];

            return users.Value
                .Select(u => new UserDto(u.Id, u.DisplayName, u.UserPrincipalName, u.CreatedDateTime))
                .ToList();
        }
    }
}
