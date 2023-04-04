using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OJP.Data.Entities;

public partial class UserDBContext : DbContext
{
    public UserDBContext()
    {
    }

    public UserDBContext(DbContextOptions<UserDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EducationDetail> EducationDetails { get; set; }

    public virtual DbSet<JobPost> JobPosts { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<RecruiterLogin> RecruiterLogins { get; set; }

    public virtual DbSet<RecruiterRegistration> RecruiterRegistrations { get; set; }

    public virtual DbSet<SeekerProfile> SeekerProfiles { get; set; }

    public virtual DbSet<SeekerRegistration> SeekerRegistrations { get; set; }

    public virtual DbSet<TechnicalDetail> TechnicalDetails { get; set; }

    public virtual DbSet<Userlog> Userlogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.2.30;Database=B23-ONLNJOBP;User ID=B23-ONLNJOBP;Password=B23-ONLNJOBP#; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EducationDetail>(entity =>
        {
            entity.ToTable("EducationDetail");

            entity.Property(e => e.CompletionDate).HasColumnType("date");
            entity.Property(e => e.Institute).HasMaxLength(200);

            entity.HasOne(d => d.Login).WithMany(p => p.EducationDetails)
                .HasForeignKey(d => d.LoginId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Education_detail_Seeker_profile");
        });

        modelBuilder.Entity<JobPost>(entity =>
        {
            entity.ToTable("JobPost");

            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.JobDescription)
                .HasMaxLength(1000)
                .IsFixedLength();
            entity.Property(e => e.JobTitle)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.JobType)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NewRegistration");

            entity.ToTable("Login");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<RecruiterLogin>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RecruiterLogin");

            entity.Property(e => e.Email)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<RecruiterRegistration>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RecruiterRegistration");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.ClientRequirement).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.ConfirmPassword).HasMaxLength(50);
            entity.Property(e => e.Designation).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Mobile)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<SeekerProfile>(entity =>
        {
            entity.HasKey(e => e.UserAccountId).HasName("PK_Seeker_profile");

            entity.ToTable("SeekerProfile");

            entity.Property(e => e.UserAccountId)
                .ValueGeneratedNever()
                .HasColumnName("User_account_id");
            entity.Property(e => e.CurrentSalary)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("current_salary");
            entity.Property(e => e.FirstName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("last_name");

            entity.HasOne(d => d.UserAccount).WithOne(p => p.SeekerProfile)
                .HasForeignKey<SeekerProfile>(d => d.UserAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Seeker_profile_User_Account");
        });

        modelBuilder.Entity<SeekerRegistration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Apply");

            entity.ToTable("SeekerRegistration");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CurrentCity).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Qualification).HasMaxLength(50);
            entity.Property(e => e.Resume).HasMaxLength(50);
        });

        modelBuilder.Entity<TechnicalDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Experience_detail");

            entity.ToTable("TechnicalDetail");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.Experience).HasMaxLength(50);
            entity.Property(e => e.JobTitle).HasMaxLength(50);
            entity.Property(e => e.LoginId).ValueGeneratedOnAdd();
            entity.Property(e => e.TechSkills).HasMaxLength(50);

            entity.HasOne(d => d.Login).WithMany(p => p.TechnicalDetails)
                .HasForeignKey(d => d.LoginId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Experience_detail_Seeker_profile");
        });

        modelBuilder.Entity<Userlog>(entity =>
        {
            entity.HasKey(e => e.UserAccountId).HasName("PK_User_log");

            entity.ToTable("Userlog");

            entity.Property(e => e.UserAccountId).ValueGeneratedNever();
            entity.Property(e => e.LastJobApplyDate).HasColumnType("date");
            entity.Property(e => e.LastLoginDate).HasColumnType("date");

            entity.HasOne(d => d.UserAccount).WithOne(p => p.Userlog)
                .HasForeignKey<Userlog>(d => d.UserAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_log_User_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
