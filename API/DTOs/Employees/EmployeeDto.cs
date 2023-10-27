using API.Models;
using API.Utilities.Enums;
using System;

namespace API.DTOs.Employees;

public class EmployeeDto
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
    public Guid? AccountGuid { get; set; }

    public static explicit operator
        EmployeeDto(Employee employee) // Operator explicit untuk mengkonversi Employee menjadi EmployeeDto.
    {

        return new EmployeeDto // Mengembalikan object EmployeeDto dengan data dari property Employee.
        {
            Guid = employee.Guid,
            Nik = employee.Nik,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            AccountGuid = employee.AccountGuid
        };
    }

    public static implicit operator
        Employee(EmployeeDto employeeDto) // Operator implicit untuk mengkonversi EmployeeDto menjadi Employee.
    {

        return new Employee // Mengembalikan object Employee dengan data dari property EmployeeDto.
        {
            Guid = new Guid(),
            Nik = employeeDto.Nik,
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName,
            BirthDate = employeeDto.BirthDate,
            Gender = employeeDto.Gender,
            HiringDate = employeeDto.HiringDate,
            Email = employeeDto.Email,
            PhoneNumber = employeeDto.PhoneNumber,
            AccountGuid = employeeDto.AccountGuid
        };
    }
}