using ClinicAzure.Application.Intefaces;

namespace ClinicAzure.Infrastructure.Services
{
    public class ApplicationUser : IApplicationUser
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
    }
}
