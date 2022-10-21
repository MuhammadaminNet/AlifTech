using AlifTechTask.Data.DbContexts;
using AlifTechTask.Data.IRepositories;
using AlifTechTask.Data.Repositories;
using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.Interfaces;
using AlifTechTask.Service.Services;
using FluentAssertions;
using Force.DeepCloner;
using Microsoft.EntityFrameworkCore;

namespace AlifTechTask.Test.Unit.Services.Transactions
{
    public class TransactionServiceTest
    {
        private readonly IRepository<Transaction> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly AlifTechTaskDbContext _dbContext;
        private readonly ITransactionService _transactionService;

        public TransactionServiceTest()
        {
            var options = new DbContextOptionsBuilder<AlifTechTaskDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _dbContext = new AlifTechTaskDbContext(options);
            _repository = new Repository<Transaction>(_dbContext);
            _userRepository = new Repository<User>(_dbContext);
            _transactionService = new TransactionService(_repository, _userRepository);
        }

        /// <summary>
        /// Create a random phone, amount and id for testing our transaction service 
        /// </summary>
        /// <returns></returns>
        public (string, decimal, Guid) CreateAFakeModelForCompleateBalance()
        {
            string phone = Faker.Phone.Number();
            decimal amount = new decimal(1000);
            Guid Id = new Guid(phone);

            return (phone, amount, Id);
        }

        /// <summary>
        /// Create a random phone number
        /// </summary>
        /// <returns></returns>
        public string CreateRandomPhone()
            => Faker.Phone.Number();


        /// <summary>
        /// Tests a compleating balance
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async ValueTask ShouldCompleateBalance()
        {
            // given
            var randomInfo = CreateAFakeModelForCompleateBalance();
            var expectedInfo = randomInfo.DeepClone();

            // when 
            var newInfo = await _transactionService.CompleateBalanse(randomInfo.Item1, randomInfo.Item2, randomInfo.Item3);

            // then 
            newInfo.Should().NotBeNull();
            newInfo.Amount.Should().Equals(expectedInfo.Item2);
        }


        /// <summary>
        /// Tests that it is fetching the desired users info by entered phone
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async ValueTask ShouldGetHistory()
        {
            // given
            string randomPhone = CreateRandomPhone();
            string expectedPhone = randomPhone.DeepClone();

            // when 
            var transactions = await _transactionService.GetAllOperationsPerformedOfCurrentMonth(randomPhone);
            
            // then 
            transactions.Should().NotBeNull();
            transactions.Transactions.All(t => 
                t.Sender.Phone.Equals(expectedPhone) || t.Achiever.Phone.Equals(expectedPhone));
        }


        /// <summary>
        /// checks that it is fetching the desired user by entered phone
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async ValueTask ShouldGetBalance()
        {
            // given 
            var randomPhone = CreateRandomPhone();
            var expectedPhone = randomPhone.DeepClone();

            // when
            var user = await _transactionService.GetBalance(randomPhone);

            // then 
            user.Should().NotBeNull();
            user.Phone.Should().BeEquivalentTo(expectedPhone);
        }
    }
}
