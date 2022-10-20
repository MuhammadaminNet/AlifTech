using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Service.DTOs.Transactions;

namespace AlifTechTask.Service.Interfaces
{
    public interface ITransactionService
    {
        ValueTask<Transaction> CompleateBalanse(string phone, decimal amount, Guid id);
        ValueTask<decimal> GetBalance(string balance);
        ValueTask<TransactionViewModel> GetAllOperationsPerformedOfCurrentMonth(string phone);
        ValueTask<IEnumerable<Transaction>> GetAll();
    }
}
