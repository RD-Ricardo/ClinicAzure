namespace ClinicAzure.Shared.Dtos.AzureEntraID
{
    public record SignInDto(string? UserDisplayName, DateTimeOffset? CreatedDateTime, int? ErrorCode);
}
