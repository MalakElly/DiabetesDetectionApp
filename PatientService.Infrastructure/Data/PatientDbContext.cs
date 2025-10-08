using Microsoft.EntityFrameworkCore;
using PatientService.Core.Entities;

namespace PatientService.Infrastructure.Data
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options)
            : base(options)
        {
        }

        // Table Patients
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Exemple Fluent API (normalisation, contraintes)
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(p => p.LastName)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(p => p.DateOfBirth)
                      .IsRequired();
                entity.Property(p => p.Gender)
                      .IsRequired()
                      .HasMaxLength(10);
            });
        }
    }
}
