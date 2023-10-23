using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees;

public class CreateCustomerDto
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderLevel Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public static implicit operator
        Customer(CreateCustomerDto createCustomerDto)
    {
        return new Customer 
        {
            Guid = new Guid(),
            FirstName = createCustomerDto.FirstName,
            LastName = createCustomerDto.LastName,
            BirthDate = createCustomerDto.BirthDate,
            Gender = createCustomerDto.Gender,
            Email = createCustomerDto.Email,
            PhoneNumber = createCustomerDto.PhoneNumber
        };
    }
}