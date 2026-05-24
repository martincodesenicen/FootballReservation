using FootballReservation.Application.Common;
using FootballReservation.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace FootballReservation.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Registramos el generador de tokens vinculándolo a su interfaz de la capa Application
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        
        return services;
    }
}