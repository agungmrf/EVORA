using API.Contracts;
using API.DTOs.Employees;
using API.DTOs.PackageEvents;
using API.DTOs.TransactionEvents;
using API.Models;
using API.Utilities.Handler;
using Client.Contracts;
using Client.Models;
using Client.Repository;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Client.Repositories
{
    public class TransactionRepos : GeneralRepository<TransactionDetailDto, Guid>, ITransactionRepos
    {
        public TransactionRepos(string request = "TransactionEvent/") : base(request)
        {

        }

        public async Task<ResponseOKHandler<IEnumerable<TransactionDetailDto>>> GetbyGuid(Guid guid)
        {
            ResponseOKHandler<IEnumerable<TransactionDetailDto>> entityVM = null;

            using (var response = await httpClient.GetAsync($"{request}GetByCustomer/"+ guid))
            {
                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<IEnumerable<TransactionDetailDto>>>(apiResponse);
                return entityVM;
            }
        }
        public async Task<ResponseOKHandler<TransactionDetaillAllDto>> DetailbyGuid(Guid guid)
        {
            ResponseOKHandler<TransactionDetaillAllDto> entityVM = null;

            using (var response = await httpClient.GetAsync($"{request}detailByGuid/" + guid))
            {
                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<TransactionDetaillAllDto>>(apiResponse);
                return entityVM;
            }
        }
        public async Task<ResponseOKHandler<TransactionEventDto>> TransactionbyGuid(Guid guid)
        {
            ResponseOKHandler<TransactionEventDto> entityVM = null;

            using (var response = await httpClient.GetAsync($"{request}" + guid))
            {
                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<TransactionEventDto>>(apiResponse);
                return entityVM;
            }
        }

        public async Task<ResponseOKHandler<TransactionEventDto>> ApprovePayment(Guid guid,TransactionEventDto eventDto)
        {
            ResponseOKHandler<TransactionEventDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(eventDto), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<TransactionEventDto>>(apiResponse);
            }
            return entityVM;
        }
    }
}