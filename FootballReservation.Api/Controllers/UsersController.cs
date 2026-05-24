using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballReservation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // <-- Este atributo bloquea todo el controlador. Nadie pasa sin un JWT válido.
public class UsersController : ControllerBase
{
    [HttpGet("me")]
    public IActionResult GetMyProfile()
    {
        // Al usar [Authorize], .NET llena la propiedad "User" (ClaimsPrincipal) automáticamente con los datos encriptados del JWT.
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        return Ok(new
        {
            Message = "Acceso concedido a tu perfil",
            UserId = userId,
            Email = email,
            Role = role
        });
    }

    [HttpGet("admin-only")]
    [Authorize(Roles = "Admin")] // <-- Restricción estricta por Rol
    public IActionResult GetAdminData()
    {
        return Ok(new { Message = "Si podés leer esto, sos Administrador." });
    }
}