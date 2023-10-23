using API.Models;

namespace API.Contracts;

public interface IEmployeeRepository : IGeneralRepository<Employee>
{
    string GetLastNik(); // Method untuk mendapatkan NIK terakhir
    Employee? GetByEmail(string email); // Method untuk mendapatkan data Employee berdasarkan email
}