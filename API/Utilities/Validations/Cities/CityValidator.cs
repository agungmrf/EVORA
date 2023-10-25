using API.Data;
using API.DTOs.Cities;
using FluentValidation;

namespace API.Utilities.Validations.Cities;

public class CityValidator : AbstractValidator<CityDto>
{
    private readonly EvoraDbContext _dbContext;
    
    public CityValidator(EvoraDbContext dbContext)
    {
        _dbContext = dbContext;
        
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters"); // Validasi Name tidak boleh lebih dari 50 karakter
        
        RuleFor(p => p.Name)
            .Must(BeUniqueName)
            .WithMessage("City '{PropertyValue}' already exists in the database");
        
        RuleFor(c => c.ProvinceGuid)
            .NotEmpty();
        
        RuleFor(c => c.DistrictGuid)
            .NotEmpty();
    }
    
    private bool BeUniqueName(string name)
    {
        // Periksa apakah Name sudah ada dalam database
        return !_dbContext.City.Any(p => p.Name == name);
    }
}