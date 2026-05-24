using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FootballReservation.Application.Common;
using FootballReservation.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FootballReservation.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        // 1. Obtener los valores de configuración
        var secretKey = _configuration["JwtSettings:Secret"] 
            ?? throw new InvalidOperationException("JWT Secret no está configurado.");
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];
        var expiryMinutes = double.Parse(_configuration["JwtSettings:ExpiryInMinutes"] ?? "60");

        // 2. Definir los Claims (la información que viaja ENCRIPTADA dentro del token)
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Role, user.Role) // ¡Crucial para la autorización basada en roles!
        };

        // 3. Crear la firma digital usando la clave secreta
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 4. Crear el cuerpo del Token
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: creds
        );

        // 5. Serializar el token a una cadena de texto (String)
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}