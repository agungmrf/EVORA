
using API.DTOs.TransactionEvents;
using API.Models;

namespace API.Contracts;

public interface ITransactionRepository : IGeneralRepository<TransactionEvent>
{
    IEnumerable<TransactionDetailDto> GetAllDetailTransaction();
}