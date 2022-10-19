using AlifTechTask.Domain.Configurations;
using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.DTOs.Users;
using System.Linq.Expressions;

namespace AlifTechTask.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<User> CreateAsync(UserForCreationDto dto);
        ValueTask<User> UpdateAsync(Expression<Func<User, bool>> expression, UserForCreationDto dto);
        ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        ValueTask<User> GetAsync(Expression<Func<User, bool>> expression);
        ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression);
    }
}
