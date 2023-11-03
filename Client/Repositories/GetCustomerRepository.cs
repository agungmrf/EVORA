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
    public class GetCustomerRepository : GeneralRepository<CustomerDto, Guid>, IGetCustomerRepository
    {
        public GetCustomerRepository(string request = "Customer/") : base(request)
        {

        }

        public async Task<ResponseOKHandler<CustomerDto>> GetbyEmail(string email)
        {
            ResponseOKHandler<CustomerDto> entityVM = null;
            using (var response = await httpClient.GetAsync(request+ "email/" + email))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<CustomerDto>>(apiResponse);
            }
            return entityVM;
        }
    }
}
