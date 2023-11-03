using API.DTOs.TransactionEvents;

namespace Client.Contracts
{
    public interface IAddOrderRepos : IRepository<InsertOrderTransactionDto, Guid>
    {
    }
}
