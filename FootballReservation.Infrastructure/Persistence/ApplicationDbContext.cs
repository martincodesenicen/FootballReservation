using System.Reflection;
using FootballReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballReservation.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Field> Fields => Set<Field>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Esta línea es mágica: busca automáticamente todas las clases que implementen 
        // IEntityTypeConfiguration en este proyecto de infraestructura y las aplica.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}