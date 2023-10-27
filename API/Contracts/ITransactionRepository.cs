using API.Models;

namespace API.Contracts;

public interface ITransactionRepository : IGeneralRepository<TransactionEvent>
{
<<<<<<< Updated upstream
    
=======
    IEnumerable<TransactionDetailDto> GetAllDetailTransaction();
    string GetLastTransactionByYear(string year);
>>>>>>> Stashed changes
}