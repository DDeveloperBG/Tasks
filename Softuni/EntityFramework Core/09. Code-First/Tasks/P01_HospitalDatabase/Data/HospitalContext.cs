using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientMedicament> PatientMedicaments { get; set; }
        public DbSet<DoctorsAuthentication> DoctorsAuthentications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = .; Database = Hospital; Integrated Security = true");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Patient>()
                .Property(x => x.Email)
                .IsUnicode(false);

            modelBuilder
                .Entity<Visitation>(e =>
                {
                    e.HasOne(x => x.Doctor)
                        .WithMany(x => x.Visitations);

                    e.HasOne(x => x.Patient)
                        .WithMany(x => x.Visitations);
                });

            modelBuilder
              .Entity<Diagnose>()
              .HasOne(x => x.Patient)
              .WithMany(x => x.Diagnoses);

            modelBuilder
               .Entity<PatientMedicament>()
               .HasKey(nameof(PatientMedicament.PatientId), nameof(PatientMedicament.MedicamentId));

            modelBuilder
                .Entity<DoctorsAuthentication>()
                .HasOne(x => x.Doctor);

            base.OnModelCreating(modelBuilder);
        }
    }
}
