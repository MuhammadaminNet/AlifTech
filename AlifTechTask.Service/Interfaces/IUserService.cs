using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.DTOs.Users;
using System.Linq.Expressions;

namespace AlifTechTask.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<User> CreateAsync(string phone, string password);
        ValueTask<User> UpdateAsync(Expression<Func<User, bool>> expression, UserForIdentifyDto dto);
        ValueTask<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> expression = null);
    }
}
