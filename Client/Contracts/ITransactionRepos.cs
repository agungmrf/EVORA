using API.DTOs.Employees;
using API.DTOs.PackageEvents;
using API.DTOs.TransactionEvents;
using API.Models;
using API.Utilities.Handler;

namespace Client.Contracts
{
    public interface ITransactionRepos : IRepository<TransactionDetailDto, Guid>
    {
        Task<ResponseOKHandler<IEnumerable<TransactionDetailDto>>> GetbyGuid(Guid guid);
        Task<ResponseOKHandler<TransactionDetaillAllDto>> DetailbyGuid(Guid guid);
        Task<ResponseOKHandler<TransactionEventDto>> TransactionbyGuid(Guid guid);
        Task<ResponseOKHandler<TransactionEventDto>> ApprovePayment(Guid guid, TransactionEventDto eventDto);
    }
}