using FootballReservation.Application.DTOs;

namespace FootballReservation.Application.Services;

public interface IReservationService
{
    Task<ReservationDto> CreateReservationAsync(int userId, CreateReservationDto dto);
    Task<IEnumerable<ReservationDto>> GetMyReservationsAsync(int userId);
}