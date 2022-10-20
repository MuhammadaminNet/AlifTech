using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Service.DTOs.Transactions;

namespace AlifTechTask.Service.Interfaces
{
    public interface ITransactionService
    {
        ValueTask<Transaction> CompleateBalanse(TransactionMoneyDto dto);
        ValueTask<decimal> GetBalance(string balance);
        ValueTask<IEnumerable<Transaction>> GetAllOperationsPerformedOfCurrentMonth(string year, string month);
        ValueTask<Transaction> TransferMoneyFromCardToCard(TransactionMoneyDto dto);
    }
}
