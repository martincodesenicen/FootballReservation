using FootballReservation.Application.Common;
using FootballReservation.Domain.Entities;
using FootballReservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FootballReservation.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _context;

    public ReservationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        return await _context.Reservations
            .Include(r => r.Field)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Reservation>> GetByUserIdAsync(int userId)
    {
        return await _context.Reservations
            .Include(r => r.Field)
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
    }

    public async Task<bool> IsSlotAvailableAsync(int fieldId, DateTime startDateTime, int durationInHours)
    {
        DateTime endDateTime = startDateTime.AddHours(durationInHours);

        // Buscamos si existe alguna reserva CONFIRMADA que se superponga con el horario solicitado
        bool hasOverlap = await _context.Reservations
            .AnyAsync(r => r.FieldId == fieldId && 
                           r.Status == "Confirmed" &&
                           // Lógica de superposición de intervalos de tiempo:
                           r.ReservationDate < endDateTime && 
                           r.ReservationDate.AddHours(r.DurationInHours) > startDateTime);

        // Si hay superposición, el turno NO está disponible (retorna false)
        return !hasOverlap;
    }
}