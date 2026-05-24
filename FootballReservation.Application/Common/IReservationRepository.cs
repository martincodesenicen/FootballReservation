using FootballReservation.Domain.Entities;

namespace FootballReservation.Application.Common;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync(int id);
    Task<IEnumerable<Reservation>> GetByUserIdAsync(int userId);
    Task AddAsync(Reservation reservation);
    
    // Este método es el corazón de la validación horaria
    Task<bool> IsSlotAvailableAsync(int fieldId, DateTime startDateTime, int durationInHours);
}