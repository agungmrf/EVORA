using API.DTOs.TransactionEvents;
using FluentValidation;

namespace API.Utilities.Validations.TransactionEvents;

public class TransactionEventValidator : AbstractValidator<TransactionEventDto>
{
    public TransactionEventValidator()
    {
        RuleFor(te => te.CustomerGuid)
            .NotEmpty();
        
        RuleFor(te => te.PackageGuid)
            .NotEmpty();
        
        RuleFor(te => te.LocationGuid)
            .NotEmpty();
        
        RuleFor(te => te.Invoice)
            .NotEmpty();
        
        RuleFor(te => te.EventDate)
            .NotEmpty() 
            .GreaterThanOrEqualTo(DateTime.Now); // Validasi apakah EventDate lebih besar atau sama dengan DateTime.Now

        RuleFor(te => te.Status)
            .NotNull() // Validasi apakah Status tidak null
            .IsInEnum(); // Validasi apakah Status berada di dalam enum

        RuleFor(te => te.TransactionDate)
            .NotEmpty();
            //.GreaterThanOrEqualTo(DateTime.Now); // Validasi apakah TransactionDate lebih besar atau sama dengan DateTime.Now
    }
}