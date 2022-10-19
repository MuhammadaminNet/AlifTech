using AlifTechTask.Service.DTOs.Transactions;

namespace AlifTechTask.Service.Interfaces
{
    public interface ITransactionService
    {
        ValueTask<bool> CompleateBalanse(TransactionMoneyDto dto);
        ValueTask<bool> GetBalance(Guid guid);
        ValueTask GetAllRepelishmentsOfCurrentMonth(string year, string month);
    }
}
