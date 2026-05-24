namespace FootballReservation.Domain.Entities;

public class Field
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Ej: "Cancha 1"
    public string Capacity { get; set; } = string.Empty; // Ej: "Fútbol 5", "Fútbol 7"
    public decimal PricePerHour { get; set; }
    public bool IsActive { get; set; } = true;

    // Propiedad de navegación
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}