using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees;

public class CustomerDto
{
    public Guid Guid { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderLevel Gender { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Guid? AccountGuid { get; set; }

    public static explicit operator
        CustomerDto(Customer customer) // Operator explicit untuk mengkonversi Customer menjadi CustomerDto.
    {
        return new CustomerDto // Mengembalikan object CustomerDto dengan data dari property Customer.
        {
            Guid = customer.Guid,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            BirthDate = customer.BirthDate,
            Gender = customer.Gender,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            AccountGuid = customer.AccountGuid
        };
    }

    public static implicit operator
        Customer(CustomerDto customerDto) // Operator implicit untuk mengkonversi CustomerDto menjadi Customer.
    {
        return new Customer // Mengembalikan object Customer dengan data dari property CustomerDto.
        {
            Guid = customerDto.Guid,
            FirstName = customerDto.FirstName,
            LastName = customerDto.LastName,
            BirthDate = customerDto.BirthDate,
            Gender = customerDto.Gender,
            Email = customerDto.Email,
            PhoneNumber = customerDto.PhoneNumber,
            AccountGuid = customerDto.AccountGuid
        };
    }
}