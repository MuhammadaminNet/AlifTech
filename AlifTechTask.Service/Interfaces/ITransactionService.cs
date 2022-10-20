using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Service.DTOs.Transactions;

namespace AlifTechTask.Service.Interfaces
{
    public interface ITransactionService
    {
        ValueTask<Transaction> CompleateBalanse(string phone, decimal amount);
        ValueTask<decimal> GetBalance(string balance);
        ValueTask<TransactionViewModel> GetAllOperationsPerformedOfCurrentMonth(string phone);
        ValueTask<Transaction> TransferMoneyFromCardToCard(TransactionMoneyDto dto);

        ValueTask<IEnumerable<Transaction>> GetAll();
    }
}
