using FluentValidation;
using FootballReservation.Application.DTOs;

namespace FootballReservation.Application.Validators;

public class CreateFieldDtoValidator : AbstractValidator<CreateFieldDto>
{
    public CreateFieldDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la cancha es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

        RuleFor(x => x.Capacity)
            .NotEmpty().WithMessage("La capacidad es obligatoria (ej. 'Fútbol 5').");

        RuleFor(x => x.PricePerHour)
            .GreaterThan(0).WithMessage("El precio por hora debe ser mayor a 0.");
    }
}