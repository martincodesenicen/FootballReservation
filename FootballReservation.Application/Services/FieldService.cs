using FootballReservation.Application.Common;
using FootballReservation.Application.DTOs;
using FootballReservation.Domain.Entities;

namespace FootballReservation.Application.Services;

public class FieldService : IFieldService
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IApplicationDbContext _context; // Lo usamos para confirmar la transacción con SaveChangesAsync

    public FieldService(IFieldRepository fieldRepository, IApplicationDbContext context)
    {
        _fieldRepository = fieldRepository;
        _context = context;
    }

    public async Task<IEnumerable<FieldDto>> GetAllFieldsAsync()
    {
        var fields = await _fieldRepository.GetAllActiveAsync();
        
        // Mapeo manual (Pragmático para el MVP, luego si querés podemos meter AutoMapper)
        return fields.Select(f => new FieldDto
        {
            Id = f.Id,
            Name = f.Name,
            Capacity = f.Capacity,
            PricePerHour = f.PricePerHour,
            IsActive = f.IsActive
        });
    }

    public async Task<FieldDto?> GetFieldByIdAsync(int id)
    {
        var f = await _fieldRepository.GetByIdAsync(id);
        if (f == null) return null;

        return new FieldDto
        {
            Id = f.Id,
            Name = f.Name,
            Capacity = f.Capacity,
            PricePerHour = f.PricePerHour,
            IsActive = f.IsActive
        };
    }

    public async Task<FieldDto> CreateFieldAsync(CreateFieldDto dto)
    {
        var field = new Field
        {
            Name = dto.Name,
            Capacity = dto.Capacity,
            PricePerHour = dto.PricePerHour,
            IsActive = true
        };

        await _fieldRepository.AddAsync(field);
        await _context.SaveChangesAsync(); // Persistimos en BD a través de la unidad de trabajo implícita

        return new FieldDto
        {
            Id = field.Id,
            Name = field.Name,
            Capacity = field.Capacity,
            PricePerHour = field.PricePerHour,
            IsActive = field.IsActive
        };
    }
}