using FootballReservation.Application.DTOs;

namespace FootballReservation.Application.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto registerDto);
    Task<string> LoginAsync(LoginDto loginDto);
}