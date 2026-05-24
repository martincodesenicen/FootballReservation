using FootballReservation.Application.DTOs;
using FootballReservation.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballReservation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var token = await _authService.RegisterAsync(registerDto);
            
            // Retornamos un estado 200 OK junto con el Token generado
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            // Captura temporal de errores de negocio (ej: email duplicado)
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var token = await _authService.LoginAsync(loginDto);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}