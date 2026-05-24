using FootballReservation.Application.Common; // Usamos la interfaz
using FootballReservation.Application.DTOs;
using FootballReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballReservation.Application.Services;

public class AuthService : IAuthService
{
    private readonly IApplicationDbContext _context; // <-- Ahora es la interfaz
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public Task<string> LoginAsync(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Email == registerDto.Email);
        if (userExists)
        {
            throw new Exception("El email ya se encuentra registrado.");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        var user = new User
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            PasswordHash = passwordHash,
            Role = "Client"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(); // <-- Sigue funcionando igual

        return _jwtTokenGenerator.GenerateToken(user);
    }
}