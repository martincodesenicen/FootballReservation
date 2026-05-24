namespace FootballReservation.Application.DTOs;

public class FieldDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Capacity { get; set; } = string.Empty;
    public decimal PricePerHour { get; set; }
    public bool IsActive { get; set; }
}