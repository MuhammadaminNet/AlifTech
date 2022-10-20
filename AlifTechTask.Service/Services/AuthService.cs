using AlifTechTask.Data.IRepositories;
using AlifTechTask.Domain.Enums;
using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.DTOs.Users;
using AlifTechTask.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlifTechTask.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<User> _userRepository;

        public AuthService(IConfiguration configuration, IRepository<User> repository) =>
            (_configuration, _userRepository) = (configuration, repository);

        public async Task<string> GenerateToken(UserForLoginDto dto)
        {
            User user = await _userRepository.GetAsync(u => u.Phone == dto.Phone 
                        && u.Password == dto.Password && u.State != ItemState.Deleted);

            if (user is null)
                throw new Exception("Login or Password is incorrect");

            byte[] tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            SecurityTokenDescriptor tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(byte.Parse(_configuration["JWT:lifetime"])),
                Issuer = _configuration["JWT:Issuer"],
                SigningCredentials = new SigningCredentials(
                                     new SymmetricSecurityKey(tokenKey),
                                     SecurityAlgorithms.HmacSha256Signature)
            };
            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescription);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
