using API.Contracts;
using API.DTOs.Employees;
using API.DTOs.PackageEvents;
using API.Models;
using API.Utilities.Handler;
using Client.Contracts;
using Client.Repository;
using Newtonsoft.Json;

namespace Client.Repositories
{
    public class GetEmployeeRepository : GeneralRepository<EmployeeDto, Guid>, IGetEmployeeRepository
    {
        public GetEmployeeRepository(string request = "Employee/") : base(request)
        {

        }

        public async Task<ResponseOKHandler<EmployeeDto>> GetbyEmail(string email)
        {
            ResponseOKHandler<EmployeeDto> entityVM = null;
            using (var response = await httpClient.GetAsync(request+ "email/" + email))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<EmployeeDto>>(apiResponse);
            }
            return entityVM;
        }
    }
}
