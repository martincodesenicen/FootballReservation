using FootballReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballReservation.Infrastructure.Persistence.Configurations;

public class FieldConfiguration : IEntityTypeConfiguration<Field>
{
    public void Configure(EntityTypeBuilder<Field> builder)
    {
        builder.ToTable("Fields");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(f => f.Capacity)
            .HasMaxLength(30)
            .IsRequired();

        // Configuración de precisión para dinero (buena práctica obligatoria)
        builder.Property(f => f.PricePerHour)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
    }
}