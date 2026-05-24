using System.Security.Claims;
using FootballReservation.Application.DTOs;
using FootballReservation.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballReservation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Bloqueado: Solo usuarios logueados pueden interactuar con reservas
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    /*
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReservationDto dto)
    {
        try
        {
            // Extraemos el ID del usuario directamente desde el token JWT autenticado
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { Message = "Usuario no válido en el token." });
            }

            var result = await _reservationService.CreateReservationAsync(userId, dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    */

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReservationDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            return Unauthorized(new { Message = "Usuario no válido en el token." });
        }

        // Ya no hay try-catch. Si el servicio lanza BadRequestException, el middleware se encarga.
        var result = await _reservationService.CreateReservationAsync(userId, dto);
        return Ok(result);
    }
    
    [HttpGet("my-bookings")]
    public async Task<IActionResult> GetMyBookings()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            return Unauthorized();
        }

        var result = await _reservationService.GetMyReservationsAsync(userId);
        return Ok(result);
    }
}