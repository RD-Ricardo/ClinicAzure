namespace ClinicAzure.Application.Intefaces
{
    public interface IApplicationUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string TenantId { get; set; }
    }
}
