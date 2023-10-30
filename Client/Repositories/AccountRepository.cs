using API.DTOs.Accounts;
using API.Utilities.Handler;
using Client.Contracts;
using Client.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Client.Repository
{
    public class AccountRepository : GeneralRepository<AccountDto, Guid>, IAccountRepos
    {

        public AccountRepository(string request = "Account/") : base(request)
        {
        }

        public async Task<ResponseOKHandler<ClaimsDto>> GetClaims(string token)
        {
            ResponseOKHandler<ClaimsDto> entityVM = null;
            using (var response = await httpClient.GetAsync(request + "GetClaims/" + token))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<ClaimsDto>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOKHandler<TokenDto>> Login(LoginDto login)
        {
            string jsonEntity = JsonConvert.SerializeObject(login);
            StringContent content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync($"{request}login", content))
            {
                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();
                var entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<TokenDto>>(apiResponse);
                return entityVM;
            }
        }
        public async Task<ResponseOKHandler<TokenDto>> GetGuidbyEmail(string email)
        {
            string jsonEntity = JsonConvert.SerializeObject(email);
            StringContent content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync($"{request}login", content))
            {
                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();
                var entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<TokenDto>>(apiResponse);
                return entityVM;
            }
        }
    }
}
