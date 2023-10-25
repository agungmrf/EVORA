using API.DTOs.Provinces;
using FluentValidation;

namespace API.Utilities.Validations.Provinces;

public class ProvinceValidator : AbstractValidator<ProvinceDto>
{
    public ProvinceValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters"); // Validasi Name tidak boleh lebih dari 50 karakter
    }
}