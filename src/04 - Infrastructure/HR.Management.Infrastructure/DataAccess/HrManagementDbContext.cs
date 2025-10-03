using HR.Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HR.Management.Infrastructure.DataAccess;

public sealed class HrManagementDbContext(DbContextOptions<HrManagementDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasIndex(u => u.Email)
                .IsUnique();

            entity.ToTable("Users");
        });
    }
}
