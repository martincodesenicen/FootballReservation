using FootballReservation.Application.Common;
using FootballReservation.Application.DTOs;
using FootballReservation.Domain.Entities;
using FootballReservation.Domain.Exceptions;

namespace FootballReservation.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IFieldRepository _fieldRepository;
    private readonly IApplicationDbContext _context;

    public ReservationService(
        IReservationRepository reservationRepository, 
        IFieldRepository fieldRepository, 
        IApplicationDbContext context)
    {
        _reservationRepository = reservationRepository;
        _fieldRepository = fieldRepository;
        _context = context;
    }

    public async Task<ReservationDto> CreateReservationAsync(int userId, CreateReservationDto dto)
    {
        // 1. Validar que la cancha exista
        var field = await _fieldRepository.GetByIdAsync(dto.FieldId);
        if (field == null || !field.IsActive)
        {
            throw new NotFoundException("La cancha seleccionada no existe o no está disponible.");
        }

        // 2. Validar que el horario no esté ocupado
        var isAvailable = await _reservationRepository.IsSlotAvailableAsync(dto.FieldId, dto.ReservationDate, dto.DurationInHours);
        if (!isAvailable)
        {
            throw new BadRequestException("El horario seleccionado ya se encuentra reservado.");
        }

        // 3. Calcular el precio total en el Backend (Regla de negocio)
        decimal totalPrice = field.PricePerHour * dto.DurationInHours;

        // 4. Instanciar la entidad
        var reservation = new Reservation
        {
            UserId = userId,
            FieldId = dto.FieldId,
            ReservationDate = dto.ReservationDate,
            DurationInHours = dto.DurationInHours,
            TotalPrice = totalPrice,
            Status = "Confirmed"
        };

        // 5. Persistir
        await _reservationRepository.AddAsync(reservation);
        await _context.SaveChangesAsync();

        return new ReservationDto
        {
            Id = reservation.Id,
            FieldId = reservation.FieldId,
            FieldName = field.Name,
            ReservationDate = reservation.ReservationDate,
            DurationInHours = reservation.DurationInHours,
            TotalPrice = reservation.TotalPrice,
            Status = reservation.Status
        };
    }

    public async Task<IEnumerable<ReservationDto>> GetMyReservationsAsync(int userId)
    {
        var reservations = await _reservationRepository.GetByUserIdAsync(userId);
        
        return reservations.Select(r => new ReservationDto
        {
            Id = r.Id,
            FieldId = r.FieldId,
            FieldName = r.Field?.Name ?? "Cancha Desconocida",
            ReservationDate = r.ReservationDate,
            DurationInHours = r.DurationInHours,
            TotalPrice = r.TotalPrice,
            Status = r.Status
        });
    }
}