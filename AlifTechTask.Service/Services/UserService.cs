using AlifTechTask.Data.IRepositories;
using AlifTechTask.Domain.Configurations;
using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.DTOs.Users;
using AlifTechTask.Service.Interfaces;
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
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async ValueTask<User> CreateAsync(UserForCreationDto dto)
        {
            var isExist = await _userRepository.GetAsync(u => u.Phone == dto.Phone) != null;

            if (isExist) throw new Exception("User alredy exist");

            User newUser = new User()
            {
                Phone = dto.Phone,
                FirstName = dto.FirstName != null ? dto.FirstName : "Unknown",
                LastName = dto.LastName != null ? dto.LastName : "Unknown",
                Password = dto.Password,
                IsIdentified = dto.IsIdentified,
                ItemState = Domain.Enums.ItemState.Created
            };

            newUser = await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();
            return newUser;
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
        /// <param name="params"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression)
        {
            var query = @params is null
                ? _userRepository.GetAll(expression)
                : _userRepository.GetAll(expression).Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize);

            return query.ToList();
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
        public async ValueTask<User> UpdateAsync(Expression<Func<User, bool>> expression, UserForCreationDto dto)
        {
            var isExist = await _userRepository.GetAsync(expression);

            if (isExist == null) throw new Exception("User not found");

            isExist.FirstName = dto.FirstName != null ? dto.FirstName : isExist.FirstName;
            isExist.LastName = dto.LastName != null ? dto.LastName : isExist.LastName;
            isExist.Phone = dto.Phone;
            isExist.Password = dto.Password;
            isExist.IsIdentified = dto.IsIdentified;

            isExist = await _userRepository.UpdateAsync(isExist);
            await _userRepository.SaveChangesAsync();
            return isExist;
        }
    }
}
