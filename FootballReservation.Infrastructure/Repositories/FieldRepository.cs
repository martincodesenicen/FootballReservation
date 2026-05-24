using FootballReservation.Application.Common;
using FootballReservation.Domain.Entities;
using FootballReservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FootballReservation.Infrastructure.Repositories;

public class FieldRepository : IFieldRepository
{
    private readonly ApplicationDbContext _context;

    public FieldRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Field?> GetByIdAsync(int id)
    {
        return await _context.Fields.FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<Field>> GetAllActiveAsync()
    {
        // En un complejo deportivo, típicamente solo queremos listar las canchas que están activas
        return await _context.Fields.Where(f => f.IsActive).ToListAsync();
    }

    public async Task AddAsync(Field field)
    {
        await _context.Fields.AddAsync(field);
    }

    public void Update(Field field)
    {
        _context.Fields.Update(field);
    }
}