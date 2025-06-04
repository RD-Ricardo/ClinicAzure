using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClinicAzure.Domain.Entities;

namespace ClinicAzure.Infrastructure.Database.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CreatedById)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(c => c.CreatedByName)
              .IsRequired()
              .HasMaxLength(200);

            builder.Property(c => c.Age)
                .IsRequired();
        }
    }
}
