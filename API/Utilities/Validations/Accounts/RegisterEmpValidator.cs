using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts;

public class RegisterEmpValidator : AbstractValidator<RegisterEmpDto>
{
    public RegisterEmpValidator()
    {
        RuleFor(e => e.FirstName)
            .NotEmpty() // Validasi agar FirstName tidak boleh kosong
            .MaximumLength(100)
            .WithMessage(
                "First Name must be at most 100 characters"); // Validasi agar FirstName tidak boleh lebih dari 100 karakter

        RuleFor(e => e.BirthDate)
            .NotEmpty() // Validasi agar BirthDate tidak boleh kosong
            .LessThanOrEqualTo(DateTime.Now.AddYears(-18)); // Validasi agar BirthDate tidak boleh kurang dari 18 tahun

        RuleFor(e => e.Gender)
            .NotNull() // Validasi agar Gender tidak null, jika menggunakan NotEmpty 0 dianggap kosong
            .IsInEnum(); // Validasi agar nilai Gender berada dalam enum GenderLevel

        RuleFor(e => e.HiringDate)
            .NotEmpty().WithMessage("Hiring Date is required"); // Validasi agar HiringDate tidak boleh kosong

        RuleFor(e => e.Email)
            .NotEmpty().WithMessage("Email is required") // Validasi agar Email tidak boleh kosong
            .EmailAddress().WithMessage("Email is not valid"); // Validasi agar Email harus sesuai format email

        RuleFor(e => e.PhoneNumber)
            .NotEmpty() // Validasi agar PhoneNumber tidak boleh kosong
            .MinimumLength(10)
            .WithMessage(
                "Phone Number must be at least 10 characters") // Validasi agar PhoneNumber tidak boleh kurang dari 10 karakter
            .MaximumLength(20)
            .WithMessage(
                "Phone Number must be at most 20 characters") // Validasi agar PhoneNumber tidak boleh lebih dari 20 karakter
            .Matches("^(^\\+62|62|^08)(\\d{3,4}-?){2}\\d{3,4}$")
            .WithMessage("Phone Number is not valid"); // Validasi agar PhoneNumber harus sesuai format nomor telepon
        
        RuleFor(e => e.Password)
            .NotEmpty().WithMessage("New password is required")
            .Matches(
                "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$") // Validasi password harus mengandung huruf besar, huruf kecil, angka, dan simbol
            .WithMessage(
                "Password must contain at least 8 characters, one uppercase, one lowercase, one number and one special case character");

        RuleFor(e => e.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required")
            .Equal(e => e.Password).WithMessage("Confirm password must be the same as new password");
    }
}