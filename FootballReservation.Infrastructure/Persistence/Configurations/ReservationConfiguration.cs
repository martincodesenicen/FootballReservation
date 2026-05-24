using FootballReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballReservation.Infrastructure.Persistence.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.TotalPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(r => r.Status)
            .HasMaxLength(20)
            .IsRequired();

        // Configuración de Relaciones (Claves Foráneas)
        
        // Un usuario tiene muchas reservas, una reserva pertenece a un usuario
        builder.HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict); 
            // Restrict: Si borras un usuario con reservas activas, la BD frena el borrado para no dejar datos huérfanos.

        // Una cancha tiene muchas reservas, una reserva pertenece a una cancha
        builder.HasOne(r => r.Field)
            .WithMany(f => f.Reservations)
            .HasForeignKey(r => r.FieldId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}