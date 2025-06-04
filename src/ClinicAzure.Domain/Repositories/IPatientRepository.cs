using ClinicAzure.Domain.Entities;

namespace ClinicAzure.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> CreateAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task<Patient> GetByIdAsync(Guid id);
        Task<List<Patient>> GetAllAsync();
    }
}
