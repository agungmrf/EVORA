using API.DTOs.Districts;
using FluentValidation;

namespace API.Utilities.Validations.District;

public class DistrictValidator : AbstractValidator<DistrictDto>
{
    public DistrictValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters"); // Validasi Name tidak boleh lebih dari 50 karakter
    }
}