using Microsoft.EntityFrameworkCore;
using PAM2Zaliczenie.Models;

namespace PAM2Zaliczenie.DAL
{
    public partial class PAM_KillersDBContext : DbContext
    {
        public PAM_KillersDBContext()
        {
        }

        public PAM_KillersDBContext(DbContextOptions<PAM_KillersDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeesSkils> EmployeesSkils { get; set; }
        public virtual DbSet<TaskType> TaskType { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Position).IsRequired();

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EmployeesSkils>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeesSkils)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeesSkils_Employee");

                entity.HasOne(d => d.TaskType)
                    .WithMany(p => p.EmployeesSkils)
                    .HasForeignKey(d => d.TaskTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeesSkils_TaskType");
            });

            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.Property(e => e.Comment).IsRequired();

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tasks_Employee");

                entity.HasOne(d => d.TaskType)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TaskTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tasks_TaskType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tasks_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                    
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.EmailAddress).IsRequired();
                entity.Property(e => e.UserAccessLevel).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
