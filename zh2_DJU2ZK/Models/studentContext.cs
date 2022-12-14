using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace zh2_DJU2ZK.Models;

public partial class studentContext : DbContext
{
    public studentContext()
    {
    }

    public studentContext(DbContextOptions<studentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Work> Works { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=bit.uni-corvinus.hu;Initial Catalog=student_work;Persist Security Info=True;User ID=hallgato;Password=Password123;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("employer");

            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.EmployerId).HasColumnName("employer_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity
                .ToTable("student");

            entity.Property(e => e.Birthdate)
                .HasColumnType("datetime")
                .HasColumnName("birthdate");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("work");

            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.EmployerId).HasColumnName("employer_id");
            entity.Property(e => e.Hours).HasColumnName("hours");
            entity.Property(e => e.IsSecondary).HasColumnName("is_secondary");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(50)
                .HasColumnName("job_title");
            entity.Property(e => e.PricePerHour).HasColumnName("price_per_hour");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.WorkId).HasColumnName("work_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
