using API.DTOs.Cities;
using FluentValidation;

namespace API.Utilities.Validations.Cities;

public class CityValidator : AbstractValidator<CityDto>
{
    public CityValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters"); // Validasi Name tidak boleh lebih dari 50 karakter
        
        RuleFor(c => c.ProvinceGuid)
            .NotEmpty();
        
        RuleFor(c => c.DistrictGuid)
            .NotEmpty();
    }
}