using AlifTechTask.Data.IRepositories;
using AlifTechTask.Domain.Models;
using AlifTechTask.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AlifTechTask.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<User> _repository;
        public async Task<string> GenereToken(string email)
        {
            return null;
        }
    }
}
