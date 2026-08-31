using Microsoft.EntityFrameworkCore;
using Automotive.Assistance.API.Models.Entities;

namespace Automotive.Assistance.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Automotive.Assistance.API.Models.Entities.ServiceProvider> ServiceProviders { get; set; }
    public DbSet<ServiceProviderUser> ServiceProviderUsers { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasIndex(e => e.Name).IsUnique();
        });
    }
}