using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetSpaManagement.DAL.Entities;
using System;
using System.Collections.Generic;

namespace PetSpaManagement.DAL;

public partial class PetSpaManagementContext : DbContext
{
    public PetSpaManagementContext()
    {
    }

    public PetSpaManagementContext(DbContextOptions<PetSpaManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingDetail> BookingDetails { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceHistory> ServiceHistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    private string GetConnectionString()

    {

        IConfiguration config = new ConfigurationBuilder()

        .SetBasePath(Directory.GetCurrentDirectory())

        .AddJsonFile("appsettings.json", true, true)

        .Build();

        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];
        //                      Coi chừng dấu cách


        return strConn;

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AED393851E3");

            entity.Property(e => e.Notes).HasMaxLength(255);

            entity.HasOne(d => d.CareStaff).WithMany(p => p.BookingCareStaffs)
                .HasForeignKey(d => d.CareStaffId)
                .HasConstraintName("FK__Bookings__CareSt__59063A47");

            entity.HasOne(d => d.Pet).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PetId)
                .HasConstraintName("FK__Bookings__PetId__571DF1D5");

            entity.HasOne(d => d.Receptionist).WithMany(p => p.BookingReceptionists)
                .HasForeignKey(d => d.ReceptionistId)
                .HasConstraintName("FK__Bookings__Recept__5812160E");
        });

        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.HasKey(e => e.BookingDetailId).HasName("PK__BookingD__8136D45AD1E02BD2");

            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__BookingDe__Booki__5BE2A6F2");

            entity.HasOne(d => d.Service).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__BookingDe__Servi__5CD6CB2B");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D801F61518");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A386E28C104");

            entity.Property(e => e.PaymentMethod).HasMaxLength(20);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Payments__Bookin__60A75C0F");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.PetId).HasName("PK__Pets__48E53862D38C1C06");

            entity.Property(e => e.Breed).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.PetName).HasMaxLength(100);
            entity.Property(e => e.Species).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Pets)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Pets__CustomerId__52593CB8");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A82BAC031");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB00AABD72F37");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ServiceName).HasMaxLength(100);
        });

        modelBuilder.Entity<ServiceHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__ServiceH__4D7B4ABD728DFF1D");

            entity.ToTable("ServiceHistory");

            entity.Property(e => e.Notes).HasMaxLength(255);

            entity.HasOne(d => d.CareStaff).WithMany(p => p.ServiceHistories)
                .HasForeignKey(d => d.CareStaffId)
                .HasConstraintName("FK__ServiceHi__CareS__66603565");

            entity.HasOne(d => d.Pet).WithMany(p => p.ServiceHistories)
                .HasForeignKey(d => d.PetId)
                .HasConstraintName("FK__ServiceHi__PetId__6477ECF3");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceHistories)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__ServiceHi__Servi__656C112C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CCDFB412A");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E45B4739F8").IsUnique();

            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
