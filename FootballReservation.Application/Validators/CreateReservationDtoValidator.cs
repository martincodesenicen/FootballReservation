using FluentValidation;
using FootballReservation.Application.DTOs;

namespace FootballReservation.Application.Validators;

public class CreateReservationDtoValidator : AbstractValidator<CreateReservationDto>
{
    public CreateReservationDtoValidator()
    {
        RuleFor(x => x.FieldId)
            .GreaterThan(0).WithMessage("Debe seleccionar una cancha válida.");

        RuleFor(x => x.ReservationDate)
            .NotEmpty().WithMessage("La fecha de reserva es obligatoria.")
            .Must(BeAFutureDate).WithMessage("La reserva debe ser para una fecha y hora futura.");

        RuleFor(x => x.DurationInHours)
            .InclusiveBetween(1, 4).WithMessage("La duración de la reserva debe ser de entre 1 y 4 horas.");
    }

    private bool BeAFutureDate(DateTime date)
    {
        return date > DateTime.UtcNow;
    }
}