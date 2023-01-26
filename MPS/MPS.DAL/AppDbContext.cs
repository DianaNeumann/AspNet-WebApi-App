using Microsoft.EntityFrameworkCore;
using MPS.DAL.Models;
using MPS.DAL.ModelsConfiguration;

namespace MPS.DAL;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Message> Messages { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
    }
}