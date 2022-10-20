using AlifTechTask.Data.IRepositories;
using AlifTechTask.Domain.Enums;
using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.DTOs.Transactions;
using AlifTechTask.Service.Extentions;
using AlifTechTask.Service.Helpers;
using AlifTechTask.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlifTechTask.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly IUserService _userService;

        public TransactionService(IRepository<Transaction> repository, IUserService userService, IRepository<User> userRepository) =>
            (_repository,_userService, _userRepository) = (repository, userService, userRepository);

        /// <summary>
        /// Replenish e-wallet account || Пополнение баланса
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="amount"></param>
        /// <returns>Returns the some of compleated transaction if all entered parametrs is right ather ways exception</returns>
        /// <exception cref="Exception"></exception>
        public async ValueTask<Transaction> CompleateBalanse(string phone, decimal amount, Guid id)
        {
            // i must create a httpcontext helper for get sender
            var sender = await _userService.GetAsync(u => u.Id == id);
            var achiever = await _userService.GetAsync(u => u.Phone == phone);

            // check sender and achiever is really exist or not
            if (sender == null || achiever == null) throw new Exception("Shtota netoo!!!");

            // checking achiever is identified or not and amount is accecptible for achiever 
            if (sender.Balance - amount < 0 
                || !achiever.IsIdentified && achiever.Balance + amount > 10000 
                || achiever.IsIdentified && achiever.Balance + amount > 100000)
                throw new Exception("Not an acceptable amount");

            // transaction
            sender.Balance -= amount;
            achiever.Balance += amount;

            // create a new transaction model 
            var transaction = new Transaction()
            {
                Amount = amount,
                SenderId = sender.Id,
                AchieverId = achiever.Id
            };
            sender.Update();
            achiever.Update();
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
            // get obj from datetime class with entered date for not select info which was performed in old months
            var currentMonth = DateTime.UtcNow;

            TransactionViewModel transaction = new TransactionViewModel();

            // get transactions of required user and current month
            transaction.Transactions = await _repository.GetAll(
                r => (r.CreatedAt.Month >= currentMonth.Month - 1 && r.CreatedAt.Month < currentMonth.Month + 1 && r.Sender.Phone == phone)
                  || (r.CreatedAt.Month >= currentMonth.Month - 1 && r.CreatedAt.Month < currentMonth.Month + 1 && r.Achiever.Phone == phone))
                    .Include("Sender").Include("Achiever").ToListAsync();

            decimal ss = new decimal(0);       
            decimal aa = new decimal(0);

            // get the total amount of straw spent and received somoni in current month
            foreach (var rr in transaction.Transactions)
            {
                if (rr.Sender.Phone == phone) ss += rr.Amount;
                else aa += rr.Amount;
            }
            transaction.SummOfSentSomoni = ss;
            transaction.SummOfAchievedSomoni = aa;

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
            // get user is from db
            var user = await _userRepository.GetAsync(
                u => u.Phone == phone && u.State != ItemState.Deleted);

            // check user is exist or not
            return user is null 
                ? throw new Exception("User not found")
                : user.Balance;
        }


        /// <summary>
        /// Transfer money from card to card
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async ValueTask<Transaction> TransferMoneyFromCardToCard(TransactionMoneyDto dto)
        {
            var sender = await _userRepository.GetAsync(u => u.Phone == dto.SPhone);
            var achiever = await _userRepository.GetAsync(u => u.Phone == dto.APhone);

            // check sender and achiever is really exist or not
            if (sender == null || achiever == null) throw new Exception("Shtota netoo!!!");

            // checking achiever is identified or not and amount is accecptible for achiever 
            if (sender.Balance - dto.Amount < 0
                || !achiever.IsIdentified && achiever.Balance + dto.Amount > 10000
                || achiever.IsIdentified && achiever.Balance + dto.Amount > 100000)
                throw new Exception("Not an acceptable amount");

            // transaction
            sender.Balance -= dto.Amount;
            achiever.Balance += dto.Amount;

            // create a new transaction model 
            var transaction = new Transaction()
            {
                Amount = dto.Amount,
                SenderId = sender.Id,
                Sender = sender,
                AchieverId = achiever.Id,
                Achiever = achiever
            };
            sender.Update();
            achiever.Update();
            transaction.Create();

            // save them to db
            await _userRepository.UpdateAsync(sender);
            await _userRepository.UpdateAsync(achiever);
            transaction = await _repository.AddAsync(transaction);
            await _repository.SaveChangesAsync();

            return transaction;             
        }

        public async ValueTask<IEnumerable<Transaction>> GetAll()
            => _repository.GetAll(t => t.ItemState != ItemState.Deleted).ToList();
    }
}
