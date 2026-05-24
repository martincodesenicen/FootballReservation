using FootballReservation.Domain.Entities;

namespace FootballReservation.Application.Common;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}