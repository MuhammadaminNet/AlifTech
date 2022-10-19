using AlifTechTask.Data.IRepositories;
using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Service.DTOs.Transactions;
using AlifTechTask.Service.Interfaces;

namespace AlifTechTask.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _repository;




        public ValueTask<bool> CompleateBalanse(TransactionMoneyDto dto)
        {
            throw new NotImplementedException();
        }

        public ValueTask GetAllRepelishmentsOfCurrentMonth(string year, string month)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> GetBalance(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
