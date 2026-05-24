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
            var token = await _authService.RegisterAsync(registerDto);
            
            // Retornamos un estado 200 OK junto con el Token generado
            return Ok(new { Token = token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
            var token = await _authService.LoginAsync(loginDto);
            return Ok(new { Token = token });
    }
}