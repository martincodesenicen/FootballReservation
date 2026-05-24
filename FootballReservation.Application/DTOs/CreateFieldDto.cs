namespace FootballReservation.Application.DTOs;

public class CreateFieldDto
{
    public string Name { get; set; } = string.Empty;
    public string Capacity { get; set; } = string.Empty; // Ej: "Fútbol 5"
    public decimal PricePerHour { get; set; }
}