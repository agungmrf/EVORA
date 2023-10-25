using API.Data;
using API.DTOs.Provinces;
using FluentValidation;

namespace API.Utilities.Validations.Provinces;

public class ProvinceValidator : AbstractValidator<ProvinceDto>
{
    private readonly EvoraDbContext _dbContext;

    public ProvinceValidator(EvoraDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters");

        RuleFor(p => p.Name)
            .Must(BeUniqueName)
            .WithMessage("Province '{PropertyValue}' already exists in the database");
    }

    private bool BeUniqueName(string name)
    {
        // Periksa apakah Name sudah ada dalam database
        return !_dbContext.Province.Any(p => p.Name == name);
    }
}