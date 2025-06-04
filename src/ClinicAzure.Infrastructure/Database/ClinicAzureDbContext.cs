using Microsoft.EntityFrameworkCore;
using ClinicAzure.Domain.Entities;
using ClinicAzure.Application.Intefaces;

namespace ClinicAzure.Infrastructure.Database
{
    public class ClinicAzureDbContext : DbContext
    {
        private readonly IApplicationUser _applicationUser;
        public ClinicAzureDbContext(
            DbContextOptions<ClinicAzureDbContext> options,
            IApplicationUser applicationUser)
            : base(options)
        {
            _applicationUser = applicationUser;
        }

        public DbSet<Patient> Patients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var tenantId = _applicationUser?.TenantId;

            modelBuilder.Entity<Patient>().HasQueryFilter(p => p.TenantId == tenantId);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicAzureDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
