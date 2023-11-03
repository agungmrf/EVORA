using API.DTOs.TransactionEvents;
using API.Models;
using Client.Contracts;
using Client.Repository;

namespace Client.Repositories
{
    public class AddOrderRepository : GeneralRepository<InsertOrderTransactionDto, Guid>, IAddOrderRepos
    {
        public AddOrderRepository(string request = "TransactionEvent/CreateOrder") : base(request)
        {

        }
    }
}
