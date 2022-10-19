using AlifTechTask.Service.DTOs.Users;

namespace AlifTechTask.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateToken(UserForLoginDto dto);
    }
}
