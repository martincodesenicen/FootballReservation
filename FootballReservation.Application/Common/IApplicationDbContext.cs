using FootballReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballReservation.Application.Common;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Field> Fields { get; }
    DbSet<Reservation> Reservations { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}