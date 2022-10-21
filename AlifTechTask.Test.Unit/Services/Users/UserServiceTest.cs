using AlifTechTask.Data.DbContexts;
using AlifTechTask.Data.IRepositories;
using AlifTechTask.Data.Repositories;
using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.DTOs.Users;
using AlifTechTask.Service.Interfaces;
using AlifTechTask.Service.Services;
using FluentAssertions;
using Force.DeepCloner;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlifTechTask.Test.Unit.Services.Users
{
    public class UserServiceTest
    {
        private readonly IRepository<User> _userRepository;
        private readonly AlifTechTaskDbContext _dbContext;
        private readonly IUserService _userService;


        public UserServiceTest()
        {
            var options = new DbContextOptionsBuilder<AlifTechTaskDbContext>()
                          .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _dbContext = new AlifTechTaskDbContext(options);
            _userRepository = new Repository<User>(_dbContext);
            _userService = new UserService(_userRepository);
        }


        public (string, string) ShouldCreateRequiredComponentsForCreateUser()
        {
            string phone = Faker.Phone.Number();
            string password = Faker.Name.FullName();

            return (phone, password);
        }


        [Fact]
        public async ValueTask ShouldCreateUser()
        {
            // given
            var user = ShouldCreateRequiredComponentsForCreateUser();
            var expectedUser = user.DeepClone();

            // when
            var newUser = await _userService.CreateAsync(user.Item1, user.Item2);

            // given
            newUser.Should().NotBeNull();
            newUser.Password.Should().BeEquivalentTo(expectedUser.Item2);
            newUser.Phone.Should().BeEquivalentTo(expectedUser.Item1);
        }
    }
}
