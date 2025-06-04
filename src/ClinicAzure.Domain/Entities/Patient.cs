using ClinicAzure.Shared.Abstractions;

namespace ClinicAzure.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string TenantId { get; set; }
        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public Patient(string name, int age, string tenantId, string createdById, string createdByName)
        {
            Name = name;
            Age = age;
            TenantId = tenantId;
            CreatedById = createdById;
            CreatedByName = createdByName;
        }
    }
}
