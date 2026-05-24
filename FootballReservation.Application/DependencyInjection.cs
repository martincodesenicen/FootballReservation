using System.Reflection;
using FluentValidation;
using FootballReservation.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FootballReservation.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IFieldService, FieldService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}