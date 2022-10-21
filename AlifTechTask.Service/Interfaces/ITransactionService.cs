using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.DTOs.Transactions;
using System.Linq.Expressions;

namespace AlifTechTask.Service.Interfaces
{
    public interface ITransactionService
    {
        ValueTask<Transaction> CompleateBalanse(string phone, decimal amount, Guid id);
        ValueTask<User> GetBalance(string balance);
        ValueTask<TransactionViewModel> GetAllOperationsPerformedOfCurrentMonth(string phone);
        ValueTask<IEnumerable<Transaction>> GetAll(Expression<Func<Transaction, bool>> expression = null);
    }
}
