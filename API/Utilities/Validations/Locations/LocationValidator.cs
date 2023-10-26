using API.DTOs.Locations;
using FluentValidation;

namespace API.Utilities.Validations.Locations;

public class LocationValidator : AbstractValidator<LocationDto>
{
    public LocationValidator()
    {
        RuleFor(l => l.Street)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters"); // Validasi Name tidak boleh lebih dari 100 karakter
        
        RuleFor(l => l.SubDistrictGuid)
            .NotEmpty();
    }
}