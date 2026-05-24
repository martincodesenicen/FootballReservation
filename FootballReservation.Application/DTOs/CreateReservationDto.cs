namespace FootballReservation.Application.DTOs;

public class CreateReservationDto
{
    public int FieldId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int DurationInHours { get; set; } = 1;
}