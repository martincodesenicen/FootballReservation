using FootballReservation.Domain.Entities;

namespace FootballReservation.Application.Common;

public interface IFieldRepository
{
    Task<Field?> GetByIdAsync(int id);
    Task<IEnumerable<Field>> GetAllActiveAsync();
    Task AddAsync(Field field);
    void Update(Field field); // EF Core tracks changes, so Update doesn't need to be Async
}