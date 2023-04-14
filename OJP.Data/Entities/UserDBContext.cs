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

    public virtual DbSet<JobApply> JobApplies { get; set; }

    public virtual DbSet<JobPost> JobPosts { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<TechnicalDetail> TechnicalDetails { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EducationDetail>(entity =>
        {
            entity.ToTable("EducationDetail");

            entity.Property(e => e.Cgpa).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Institute).HasMaxLength(200);
            entity.Property(e => e.Percentage).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Resume).HasMaxLength(50);
            entity.Property(e => e.Specilization).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EducationDetails)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_EducationDetail_Login1");
        });

        modelBuilder.Entity<JobApply>(entity =>
        {
            entity.ToTable("JobApply");

            entity.Property(e => e.AppliedAt).HasColumnType("datetime");

            entity.HasOne(d => d.ApplyByNavigation).WithMany(p => p.JobApplies)
                .HasForeignKey(d => d.ApplyBy)
                .HasConstraintName("FK_JobApply_Login");

            entity.HasOne(d => d.JobPost).WithMany(p => p.JobApplies)
                .HasForeignKey(d => d.JobPostId)
                .HasConstraintName("FK_JobApply_JobApply");
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
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.PostedByNavigation).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.PostedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobPost_Login");
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
            entity.Property(e => e.Profile).HasMaxLength(50);
        });

        modelBuilder.Entity<TechnicalDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Experience_detail");

            entity.ToTable("TechnicalDetail");

            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.Experience).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.JobTitle).HasMaxLength(50);
            entity.Property(e => e.TechSkills).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TechnicalDetails)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_TechnicalDetail_Login");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
