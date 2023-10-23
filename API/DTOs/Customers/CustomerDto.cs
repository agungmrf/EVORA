using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees;

public class CustomerDto
{
    public Guid Guid { get; set; }
    public string Nik { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderLevel Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public static explicit operator
        CustomerDto(Customer customer)
    {
        return new CustomerDto
        {
            Guid = customer.Guid,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            BirthDate = customer.BirthDate,
            Gender = customer.Gender,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber
        };
    }

    public static implicit operator
        Customer(CustomerDto customerDto) 
    {
        return new Customer 
        {
            Guid = customerDto.Guid,
            FirstName = customerDto.FirstName,
            LastName = customerDto.LastName,
            BirthDate = customerDto.BirthDate,
            Gender = customerDto.Gender,
            Email = customerDto.Email,
            PhoneNumber = customerDto.PhoneNumber
        };
    }
}