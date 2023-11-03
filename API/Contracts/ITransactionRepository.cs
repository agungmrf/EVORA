
using API.DTOs.TransactionEvents;
using API.Models;

namespace API.Contracts;

public interface ITransactionRepository : IGeneralRepository<TransactionEvent>
{
    IEnumerable<TransactionDetailDto> GetAllDetailTransaction();
    string GetLastTransactionByYear(string year);
    IEnumerable<TransactionDetailDto> GetByCustomer(Guid guid);
    TransactionDetaillAllDto DetailByGuid(Guid guid);
}