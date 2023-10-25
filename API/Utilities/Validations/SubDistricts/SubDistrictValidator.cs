using API.DTOs.SubDistricts;
using FluentValidation;

namespace API.Utilities.Validations.SubDistricts;

public class SubDistrictValidator : AbstractValidator<SubDistrictDto>
{
    public SubDistrictValidator()
    {
        RuleFor(sd => sd.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters"); // Validasi Name tidak boleh lebih dari 50 karakter
        
        RuleFor(sd => sd.DistrictGuid)
            .NotEmpty();
    }
}