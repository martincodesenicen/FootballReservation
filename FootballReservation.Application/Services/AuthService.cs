using FootballReservation.Application.Common; // Usamos la interfaz
using FootballReservation.Application.DTOs;
using FootballReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballReservation.Application.Services;

public class AuthService : IAuthService
{
    private readonly IApplicationDbContext _context; // <-- Ahora es la interfaz
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        IApplicationDbContext context,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _jwtTokenGenerator = jwtTokenGenerator;
}

    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        // 1. Buscar al usuario por su email
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        
        // 2. Si no existe, o si la contraseña no coincide, lanzamos un error genérico por seguridad.
        // Tip de seguridad: No le digas al atacante exactamente qué falló (si el email o la clave).
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            throw new Exception("Credenciales incorrectas."); // Luego lo cambiaremos por una excepción personalizada
        }

        // 3. Generar y retornar el token JWT si todo está en orden
        return _jwtTokenGenerator.GenerateToken(user);
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