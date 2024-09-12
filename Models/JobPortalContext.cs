using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WeFeedSA.Models;

public partial class JobPortalContext : DbContext
{
    public JobPortalContext()
    {
    }

    public JobPortalContext(DbContextOptions<JobPortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cv> Cvs { get; set; }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<Interest> Interests { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobApplication> JobApplications { get; set; }

    public virtual DbSet<JobSeeker> JobSeekers { get; set; }

    public virtual DbSet<JobSeekerSkill> JobSeekerSkills { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=labVMH8OX\\SQLEXPRESS;Initial Catalog=JobPortal;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cv>(entity =>
        {
            entity.HasKey(e => e.Cvid).HasName("PK__CVs__A04CFC4389598905");

            entity.ToTable("CVs");

            entity.Property(e => e.Cvid).HasColumnName("CVID");
            entity.Property(e => e.Certifications).HasColumnType("text");
            entity.Property(e => e.Education).HasColumnType("text");
            entity.Property(e => e.Experience).HasColumnType("text");
            entity.Property(e => e.JobSeekerId).HasColumnName("JobSeekerID");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Summary).HasColumnType("text");

            entity.HasOne(d => d.JobSeeker).WithMany(p => p.Cvs)
                .HasForeignKey(d => d.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CVs__JobSeekerID__5629CD9C");
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.HasKey(e => e.EmployerId).HasName("PK__Employer__CA44524140FFBFEA");

            entity.HasIndex(e => e.UserId, "idx_employers_userid");

            entity.Property(e => e.EmployerId).HasColumnName("EmployerID");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Industry)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Employers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Employers__UserI__5CD6CB2B");
        });

        modelBuilder.Entity<Interest>(entity =>
        {
            entity.HasKey(e => e.InterestId).HasName("PK__Interest__20832C070971CA39");

            entity.HasIndex(e => e.EmployerId, "idx_interests_employerid");

            entity.HasIndex(e => e.JobSeekerId, "idx_interests_jobseekerid");

            entity.Property(e => e.InterestId).HasColumnName("InterestID");
            entity.Property(e => e.DateExpressed)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmployerId).HasColumnName("EmployerID");
            entity.Property(e => e.JobSeekerId).HasColumnName("JobSeekerID");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Employer).WithMany(p => p.Interests)
                .HasForeignKey(d => d.EmployerId)
                .HasConstraintName("FK__Interests__Emplo__628FA481");

            entity.HasOne(d => d.JobSeeker).WithMany(p => p.Interests)
                .HasForeignKey(d => d.JobSeekerId)
                .HasConstraintName("FK__Interests__JobSe__6383C8BA");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__Jobs__056690E224246A71");

            entity.HasIndex(e => e.EmployerId, "idx_jobs_employerid");

            entity.Property(e => e.JobId).HasColumnName("JobID");
            entity.Property(e => e.ApplicationDeadline).HasColumnType("datetime");
            entity.Property(e => e.EmployerId).HasColumnName("EmployerID");
            entity.Property(e => e.ExperienceLevel)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.JobDescription).HasColumnType("text");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.JobType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PostedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RequiredSkills).HasColumnType("text");
            entity.Property(e => e.SalaryRange)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Employer).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.EmployerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Jobs__EmployerID__6FE99F9F");
        });

        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__JobAppli__C93A4F79BAD3183C");

            entity.HasIndex(e => e.JobId, "idx_jobapplications_jobid");

            entity.HasIndex(e => e.JobSeekerId, "idx_jobapplications_jobseekerid");

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.ApplicationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ApplicationStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending");
            entity.Property(e => e.CoverLetter).HasColumnType("text");
            entity.Property(e => e.JobId).HasColumnName("JobID");
            entity.Property(e => e.JobSeekerId).HasColumnName("JobSeekerID");

            entity.HasOne(d => d.Job).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__JobApplic__JobID__1332DBDC");

            entity.HasOne(d => d.JobSeeker).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.JobSeekerId)
                .HasConstraintName("FK__JobApplic__JobSe__14270015");
        });

        modelBuilder.Entity<JobSeeker>(entity =>
        {
            entity.HasKey(e => e.JobSeekerId).HasName("PK__JobSeeke__89113A8C8BCFFCC2");

            entity.HasIndex(e => e.UserId, "idx_jobseekers_userid");

            entity.Property(e => e.JobSeekerId).HasColumnName("JobSeekerID");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProfileStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Incomplete");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VisibilityConsent).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.JobSeekers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__JobSeeker__UserI__52593CB8");
        });

        modelBuilder.Entity<JobSeekerSkill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PK__JobSeeke__DFA091E726D5CF75");

            entity.Property(e => e.SkillId).HasColumnName("SkillID");
            entity.Property(e => e.JobSeekerId).HasColumnName("JobSeekerID");
            entity.Property(e => e.SkillLevel)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SkillName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.JobSeeker).WithMany(p => p.JobSeekerSkills)
                .HasForeignKey(d => d.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__JobSeeker__JobSe__59FA5E80");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58737377D8");

            entity.HasIndex(e => e.InterestId, "idx_payments_interestid");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.InterestId).HasColumnName("InterestID");
            entity.Property(e => e.PaymentAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Success");

            entity.HasOne(d => d.Interest).WithMany(p => p.Payments)
                .HasForeignKey(d => d.InterestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Payments__Intere__693CA210");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC3ADADBEA");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E48EE08254").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534AE3F5A61").IsUnique();

            entity.HasIndex(e => e.Email, "idx_users_email");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
