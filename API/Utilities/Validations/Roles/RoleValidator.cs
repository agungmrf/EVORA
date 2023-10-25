using API.Data;
using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles;

public class RoleValidator : AbstractValidator<RoleDto>
{
    private readonly EvoraDbContext _dbContext;
    public RoleValidator(EvoraDbContext dbContext)
    {
        _dbContext = dbContext;
        
        RuleFor(r => r.Name)
            .NotEmpty() // Validasi bahwa nama role tidak boleh kosong
            .MaximumLength(100)
            .WithMessage(
                "Role name must not exceed 100 characters."); // Validasi bahwa nama role tidak boleh lebih dari 100 karakter
        
        RuleFor(p => p.Name)
            .Must(BeUniqueName)
            .WithMessage("Role '{PropertyValue}' already exists in the database");
    }
    
    private bool BeUniqueName(string name)
    {
        // Periksa apakah Name sudah ada dalam database
        return !_dbContext.Roles.Any(p => p.Name == name);
    }
}