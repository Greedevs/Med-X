using MedX.Domain.Entities;
using MedX.Domain.Entities.Administrators;
using Microsoft.EntityFrameworkCore;

namespace MedX.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { 
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Treatment> Treatments { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Administrator> Administrators { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Doctor>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Patient>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Payment>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Treatment>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Transaction>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Appointment>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Administrator>().HasQueryFilter(u => !u.IsDeleted);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Appointment>()
            .HasOne(d => d.Doctor)
            .WithMany(a => a.Appointments)
            .HasForeignKey(a => a.DoctorId);

        modelBuilder.Entity<Appointment>()
            .HasOne(p => p.Patient)
            .WithMany(a => a.Appointments)
            .HasForeignKey(a => a.PatientId);

        modelBuilder.Entity<Transaction>()
            .HasOne(d => d.Doctor)
            .WithMany(t => t.Transactions)
            .HasForeignKey(d => d.DoctorId);

        modelBuilder.Entity<Transaction>()
            .HasOne(p => p.Patient)
            .WithMany(a => a.Transactions)
            .HasForeignKey(a => a.PatientId);

        modelBuilder.Entity<Transaction>()
            .HasOne(p => p.Payment)
            .WithMany(a => a.Transactions)
            .HasForeignKey(a => a.PaymentId);

        modelBuilder.Entity<Treatment>()
            .HasOne(p => p.Patient)
            .WithMany(t => t.Treatments)
            .HasForeignKey(p => p.PatientId);
    }
}