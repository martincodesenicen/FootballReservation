namespace FootballReservation.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "Client"; // Client, Admin
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propiedad de navegación (Relación: Un usuario puede tener muchas reservas)
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}