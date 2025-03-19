using KpoApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KpoApi.Infrastructure.PostgresEfCore.Models;

public sealed class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Organization> Organizations { get; set; } = null!;
    public DbSet<UserCardiograph> UsersCardiographs { get; set; } = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated(); 
    }
}