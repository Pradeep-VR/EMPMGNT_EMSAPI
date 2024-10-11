using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TEAMLIFTSS.Repos.TableModels;

namespace TEAMLIFTSS.Repos;

public partial class ApplicationDBContext : DbContext
{
    public ApplicationDBContext()
    {
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendancedetail> Attendancedetails { get; set; }

    public virtual DbSet<Employeedetail> Employeedetails { get; set; }

    public virtual DbSet<Refreshtoken> Refreshtokens { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=192.168.0.180;Database=EMPMGMT;User id=sa;Password=Welcome@123;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendancedetail>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__ATTENDAN__16998B6178B70781");

            entity.Property(e => e.TransactionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendancedetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLOYEEID");
        });

        modelBuilder.Entity<Employeedetail>(entity =>
        {
            entity.HasKey(e => e.Empid).HasName("PK__EMPLOYEE__14CCD97D1F5A9A43");
        });

        modelBuilder.Entity<Refreshtoken>(entity =>
        {
            entity.HasKey(e => e.Empid).HasName("PK_TBL_REFRESHTOKEN");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
