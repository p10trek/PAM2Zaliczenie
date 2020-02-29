using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PAM2Zaliczenie.Models
{
    public partial class PAM_KillersDBContext : DbContext
    {
        private string Password { get; set; }
        private string Login { get; set; }
        public PAM_KillersDBContext(string password,string login)
        {
            this.Password = password;
            this.Login = login;
        }

        public PAM_KillersDBContext(DbContextOptions<PAM_KillersDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<TaskType> TaskType { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:pamkillers.database.windows.net,1433;Initial Catalog=PAM_KillersDB;Persist Security Info=False;User ID="+Login+";Password="+Password+";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IsEmployed).HasColumnName("isEmployed");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Comment).HasColumnType("ntext");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Duration)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.StartTime).HasColumnType("smalldatetime");

                entity.Property(e => e.TaskTypeId).HasColumnName("TaskTypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
