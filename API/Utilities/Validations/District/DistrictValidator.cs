using API.Data;
using API.DTOs.Districts;
using FluentValidation;

namespace API.Utilities.Validations.District;

public class DistrictValidator : AbstractValidator<DistrictDto>
{
    private readonly EvoraDbContext _dbContext;

    public DistrictValidator(EvoraDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(d => d.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters"); // Validasi Name tidak boleh lebih dari 50 karakter

        RuleFor(p => p.Name)
            .Must(BeUniqueName)
            .WithMessage("District '{PropertyValue}' already exists in the database");

        RuleFor(c => c.CityGuid)
            .NotEmpty();
    }

    private bool BeUniqueName(string name)
    {
        // Periksa apakah Name sudah ada dalam database
        return !_dbContext.District.Any(p => p.Name == name);
    }
}