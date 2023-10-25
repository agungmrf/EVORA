using API.DTOs.PackageEvents;
using FluentValidation;

namespace API.Utilities.Validations.PackageEvents;

public class PackageEventValidator : AbstractValidator<PackageEventDto>
{
    public PackageEventValidator()
    {
        RuleFor(pe => pe.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters"); // Validasi Name tidak boleh lebih dari 100 karakter

        RuleFor(pe => pe.Capacity)
            .NotEmpty(); // Validasi bahwa Capacity tidak boleh kosong

        RuleFor(pe => pe.Description)
            .NotEmpty();

        RuleFor(pe => pe.Price)
            .NotEmpty();
    }
}