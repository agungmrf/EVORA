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

        public Task<ResponseOKHandler<TokenDto>> GetToken(string token)
        {
            throw new NotImplementedException();
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
    }
}
