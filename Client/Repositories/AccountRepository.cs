using API.DTOs.Accounts;
using API.Utilities.Handler;
using Client.Contracts;
using Client.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using API.Utilities.Validations.Accounts;

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
        
        public async Task<ResponseOKHandler<RegisterCustDto>> RegisterUser(RegisterCustDto registerCustDto)
        {
            string jsonEntity = JsonConvert.SerializeObject(registerCustDto);
            StringContent content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync($"{request}register-customer", content))
            {
                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<ResponseOKHandler<RegisterCustDto>>(apiResponse);
                return responseDto;
            }
        }
        
        public async Task<ResponseOKHandler<ForgotPasswordDto>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            string jsonEntity = JsonConvert.SerializeObject(forgotPasswordDto);
            StringContent content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync($"{request}forgot-password", content))
            {
                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<ResponseOKHandler<ForgotPasswordDto>>(apiResponse);
                return responseDto;
            }
        }
        
        public async Task<ResponseOKHandler<ChangePasswordDto>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            string jsonEntity = JsonConvert.SerializeObject(changePasswordDto);
            StringContent content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync($"{request}change-password", content))
            {
                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<ResponseOKHandler<ChangePasswordDto>>(apiResponse);
                return responseDto;
            }
        }
    }
}
