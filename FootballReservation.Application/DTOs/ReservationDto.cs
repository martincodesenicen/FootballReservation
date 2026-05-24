namespace FootballReservation.Application.DTOs;

public class ReservationDto
{
    public int Id { get; set; }
    public int FieldId { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public DateTime ReservationDate { get; set; }
    public int DurationInHours { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = string.Empty;
}