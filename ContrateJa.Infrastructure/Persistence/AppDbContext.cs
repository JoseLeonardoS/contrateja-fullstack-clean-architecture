using ContrateJa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContrateJa.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options) { }
    
    public DbSet<User>  Users { get; set; }
    public DbSet<Job>  Jobs { get; set; }
    public DbSet<Proposal>  Proposals { get; set; }
    public DbSet<Review>  Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}