using AlifTechTask.Data.IRepositories;
using AlifTechTask.Domain.Enums;
using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.DTOs.Transactions;
using AlifTechTask.Service.Interfaces;

namespace AlifTechTask.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly IUserService _userService;

        public TransactionService(IRepository<Transaction> repository, IUserService userService) =>
            (_repository,_userService) = (repository, userService);

        /// <summary>
        /// Replenish e-wallet account || Пополнение баланса
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Returns the some of compleated transaction if all entered parametrs is right ather ways exception</returns>
        /// <exception cref="Exception"></exception>
        public async ValueTask<Transaction> CompleateBalanse(TransactionMoneyDto dto)
        {
            // i must create a httpcontext helper for get sender
            var sender = await _userService.GetAsync(u => u.Id.ToString() == "take user id from http context accessr");
            var achiever = await _userService.GetAsync(u => u.Phone == dto.Phone);

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

            // create new entity model transaction
            var transaction = new Transaction()
            {
                Amount = dto.Amount,
                SenderId = sender.Id,
                AchieverId = achiever.Id
            };

            // save them to db
            await _userRepository.UpdateAsync(sender);
            await _userRepository.UpdateAsync(achiever);
            return await _repository.AddAsync(transaction);
        }
        
        /// <summary>
        /// Get all operations performed in the current month
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>Collection of transactions</returns>
        /// <exception cref="Exception"></exception>
        public async ValueTask<IEnumerable<Transaction>> GetAllOperationsPerformedOfCurrentMonth(string year, string month)
        {
            // checking input date is valid or not
            if (!year.All(char.IsDigit) || month.All(char.IsDigit))
                throw new Exception("Input format is not correct");

            // convert input params to int
            var year2 = int.Parse(year);
            var month2 = int.Parse(month);

            // get obj from datetime class with entered date
            var monthOfNeed = new DateTime(year2, month2, 0);
            
            // month and year creadiantials
            if (month2 == 12)
                (year2, month2) = (year2 + 1, 1);

            // get obj from datetime class for get only current month info
            var lastMonth = new DateTime(year2, month2, 1);

            // get transactions of current month
            return _repository.GetAll(r => r.CreatedAt >= monthOfNeed && r.CreatedAt < lastMonth).ToList();
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
    }
}
