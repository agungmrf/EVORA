using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.DTOs.TransactionEvents;
using API.Utilities.Handler;
using Client.Models;

namespace Client.Contracts
{
    public interface IAccountRepos : IRepository<AccountDto, Guid>
    {
        Task<ResponseOKHandler<TokenDto>> Login(LoginDto login);
        Task<ResponseOKHandler<ClaimsDto>> GetClaims(string token);
    }
}
