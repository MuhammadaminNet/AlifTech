using AlifTechTask.Data.IRepositories;
using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.DTOs.Users;
using AlifTechTask.Service.Extentions;
using AlifTechTask.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AlifTechTask.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository) =>
            (_userRepository) = (userRepository);


        /// <summary>
        /// Create a new account for user if user already not exist
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async ValueTask<User> CreateAsync(string phone, string password)
        {
            var user = await _userRepository.GetAsync(u => u.Phone == phone);

            if (phone.Contains('+'))
                phone = phone.Substring(1, phone.Length - 1);

            if (!phone.All(char.IsDigit))
                throw new Exception("Number is not korrect format");

            if (user != null) throw new Exception("User alredy exist");

            user = new User();
            user.Phone = phone;
            user.Password = password;
            user.Create();

            user = await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return user;
        }


        /// <summary>
        /// Delete a account by entered expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var user = await _userRepository.GetAsync(expression);

            if (user == null) return false;

            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveChangesAsync(); 
            return true;
        }


        /// <summary>
        /// Select all users
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async ValueTask<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> expression = null)
            => expression is not null
               ? (await _userRepository.GetAll(expression).ToListAsync())
               : (await _userRepository.GetAll().ToListAsync());


        /// <summary>
        /// Select user by entered expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async ValueTask<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            var user = await _userRepository.GetAsync(expression);
            return user != null ? user : throw new Exception("User not fount");
        }


        /// <summary>
        /// Update user to entered dto by entered expression
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async ValueTask<User> UpdateAsync(Expression<Func<User, bool>> expression, UserForIdentifyDto dto)
        {
            var user = await _userRepository.GetAsync(expression);

            if (user == null) throw new Exception("User not found");

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.IsIdentified = true;
            user.Update();

            user = await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
            return user;
        }
    }
}
