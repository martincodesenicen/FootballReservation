using FootballReservation.Application.DTOs;

namespace FootballReservation.Application.Services;

public interface IFieldService
{
    Task<IEnumerable<FieldDto>> GetAllFieldsAsync();
    Task<FieldDto?> GetFieldByIdAsync(int id);
    Task<FieldDto> CreateFieldAsync(CreateFieldDto createFieldDto);
}