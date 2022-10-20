using AlifTechTask.Data.IRepositories;
using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.DTOs.Transactions;
using AlifTechTask.Service.Extentions;
using AlifTechTask.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlifTechTask.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _repository;
        private readonly IRepository<User> _userRepository;

        public TransactionService(IRepository<Transaction> repository, IRepository<User> userRepository)
            => (_repository, _userRepository) = (repository, userRepository);


        /// <summary>
        /// Replenish e-wallet account || Пополнение баланса
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="amount"></param>
        /// <returns>Returns the some of compleated transaction if all entered parametrs is right ather ways exception</returns>
        /// <exception cref="Exception"></exception>
        public async ValueTask<Transaction> CompleateBalanse(string phone, decimal amount, Guid id)
        {
            var sender = await _userRepository.GetAsync(u => u.Id == id);
            var achiever = await _userRepository.GetAsync(u => u.Phone == phone);

            // check sender and achiever is really exist or not
            if (sender == null || achiever == null) throw new Exception("Shtota netoo!!!");

            // checking achiever is identified or not and amount is accecptible for achiever 
            if (sender.Balance - amount < 0 
                || !achiever.IsIdentified && achiever.Balance + amount > 10000 
                || achiever.IsIdentified && achiever.Balance + amount > 100000)
                throw new Exception("Not an acceptable amount");

            // transaction
            sender.Update();
            achiever.Update();
            sender.Balance -= amount;
            achiever.Balance += amount;

            // create a new transaction model 
            var transaction = new Transaction();
            transaction.Amount = amount;
            transaction.SenderId = sender.Id;
            transaction.AchieverId = achiever.Id;
            transaction.Create();

            // writing them to db
            await _userRepository.UpdateAsync(sender);
            await _userRepository.UpdateAsync(achiever);
            transaction = await _repository.AddAsync(transaction);

            // save changes to database
            await _repository.SaveChangesAsync();
            return transaction;
        }


        /// <summary>
        /// Get all operations performed in the current month
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async ValueTask<TransactionViewModel> GetAllOperationsPerformedOfCurrentMonth(string phone)
        {
            // get obj from required class 
            var currentMonth = DateTime.UtcNow;
            TransactionViewModel transaction = new TransactionViewModel();

            // get transactions of required user and current month
            transaction.Transactions = await _repository.GetAll(r => 
                     (r.CreatedAt.Month >= currentMonth.Month - 1 && r.CreatedAt.Month < currentMonth.Month + 1 && r.Sender.Phone == phone) || 
                     (r.CreatedAt.Month >= currentMonth.Month - 1 && r.CreatedAt.Month < currentMonth.Month + 1 && r.Achiever.Phone == phone))
                     .Include("Sender").Include("Achiever").ToListAsync();

            // get the total amount of spent and achieved somoni and count of operations in current month
            foreach (var rr in transaction.Transactions)
            {
                if (rr.Sender.Phone == phone) transaction.SummOfSentSomoni += rr.Amount;
                else transaction.SummOfAchievedSomoni += rr.Amount;
                transaction.CountOfOperations += 1;
            }

            return transaction;
        }


        /// <summary>
        /// Get info about balance of user who has entered number
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>decimal balans</returns>
        /// <exception cref="Exception"></exception>
        public async ValueTask<decimal> GetBalance(string phone)
        {
            var user = await _userRepository.GetAsync(u => u.Phone == phone);

            return user is null 
                ? throw new Exception("User not found") 
                : user.Balance;
        }

        public async ValueTask<IEnumerable<Transaction>> GetAll()
            => await _repository.GetAll().ToListAsync();
    }
}
