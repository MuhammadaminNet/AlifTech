using AlifTechTask.Data.IRepositories;
using AlifTechTask.Domain.Enums;
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
            var isExist = await _userRepository.GetAsync(u => u.Phone == phone);

            if (isExist != null) throw new Exception("User alredy exist");

            isExist = new User();
            isExist.Phone = phone;
            isExist.Password = password;
            isExist.Create();

            isExist = await _userRepository.AddAsync(isExist);
            await _userRepository.SaveChangesAsync();
            return isExist;
        }


        /// <summary>
        /// Delete a account by entered expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var isExist = await _userRepository.GetAsync(expression);

            if (isExist == null) return false;

            await _userRepository.DeleteAsync(isExist);
            await _userRepository.SaveChangesAsync(); 

            return true;
        }


        /// <summary>
        /// Select all users
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async ValueTask<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> expression = null)
        {
            var query = expression is not null
                ? _userRepository.GetAll(expression)
                : _userRepository.GetAll(u => u.State != ItemState.Deleted);

            return await query.ToListAsync();
        }


        /// <summary>
        /// Select user by entered expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async ValueTask<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            var isExist = await _userRepository.GetAsync(expression);

            if(isExist == null) throw new Exception("User not found");

            return isExist;
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
            var isExist = await _userRepository.GetAsync(expression);

            if (isExist == null) throw new Exception("User not found");

            isExist.FirstName = dto.FirstName;
            isExist.LastName = dto.LastName;
            isExist.IsIdentified = true;
            isExist.Update();

            isExist = await _userRepository.UpdateAsync(isExist);
            await _userRepository.SaveChangesAsync();
            return isExist;
        }
    }
}
