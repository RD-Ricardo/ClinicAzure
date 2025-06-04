namespace ClinicAzure.Shared.Dtos.AzureEntraID
{
    public record UserDto(string? Id, string? DisplayName, string? UserPrincipalName, DateTimeOffset? CreatedAt);
}
