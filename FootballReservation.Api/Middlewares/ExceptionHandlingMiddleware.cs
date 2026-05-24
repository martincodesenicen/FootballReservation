using System.Net;
using System.Text.Json;
using FootballReservation.Domain.Exceptions;

namespace FootballReservation.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Deja que la petición siga su curso normal
            await _next(context);
        }
        catch (Exception ex)
        {
            // Si algo falla en cualquier capa inferior, se captura acá de forma centralizada
            _logger.LogError(ex, "Ocurrió un error no controlado: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        // Evaluamos el tipo de excepción para asignar el código de estado HTTP correcto
        context.Response.StatusCode = exception switch
        {
            BadRequestException => (int)HttpStatusCode.BadRequest,   // 400
            NotFoundException => (int)HttpStatusCode.NotFound,       // 404
            _ => (int)HttpStatusCode.InternalServerError             // 500 (Errores inesperados del servidor)
        };

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
            // En producción nunca querrás mostrar el StackTrace detallado al cliente, 
            // pero para el MVP o desarrollo podrías mapearlo condicionalmente.
            Detail = context.Response.StatusCode == 500 ? "Error interno del servidor." : null
        };

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
}