using MedX.Domain.Entities;
using MedX.Domain.Entities.Services;
using Microsoft.EntityFrameworkCore;
using MedX.Domain.Entities.Appointments;
using MedX.Domain.Entities.Administrators;
using MedX.Domain.Entities.MedicalRecords;

namespace MedX.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { 
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Affair> Affairs { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<CashDesk> CashDesks { get; set; }
    public DbSet<Treatment> Treatments { get; set; }
    public DbSet<AffairItem> AffairItems { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
    public DbSet<Administrator> Administrators { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region entitylar uchun "isDeleted" holatini filter qilish
        modelBuilder.Entity<Room>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Affair>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Doctor>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Patient>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Payment>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<CashDesk>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Treatment>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<AffairItem>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Appointment>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Administrator>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<MedicalRecord>().HasQueryFilter(u => !u.IsDeleted);
        #endregion

        #region Fluent API 
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>()
            .Property(u => u.DateOfBirth)
            .HasColumnType("timestamp");

        modelBuilder.Entity<Appointment>()
            .HasOne(d => d.Doctor)
            .WithMany(a => a.Appointments)
            .HasForeignKey(a => a.DoctorId);

        modelBuilder.Entity<Appointment>()
            .HasOne(p => p.Patient)
            .WithMany(a => a.Appointments)
            .HasForeignKey(a => a.PatientId);

        modelBuilder.Entity<MedicalRecord>()
            .HasOne(d => d.Doctor)
            .WithMany(t => t.MedicalRecords)
            .HasForeignKey(d => d.DoctorId);

        modelBuilder.Entity<MedicalRecord>()
            .HasOne(p => p.Patient)
            .WithMany(a => a.MedicalRecords)
            .HasForeignKey(a => a.PatientId);

        modelBuilder.Entity<Treatment>()
            .HasOne(p => p.Patient)
            .WithMany(t => t.Treatments)
            .HasForeignKey(p => p.PatientId);

        modelBuilder.Entity<AffairItem>()
            .HasOne(p => p.Patient)
            .WithMany(t => t.AffairItems)
            .HasForeignKey(p => p.PatientId);
        #endregion
    }
}