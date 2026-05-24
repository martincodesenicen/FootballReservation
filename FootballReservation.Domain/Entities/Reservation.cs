namespace FootballReservation.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int FieldId { get; set; }
    
    // Almacenamos la fecha y la hora exacta de inicio de la reserva
    public DateTime ReservationDate { get; set; } 
    public int DurationInHours { get; set; } = 1; // Por defecto 1 hora
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = "Confirmed"; // Confirmed, Cancelled
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propiedades de navegación de EF Core (Relaciones)
    public User User { get; set; } = null!;
    public Field Field { get; set; } = null!;
}