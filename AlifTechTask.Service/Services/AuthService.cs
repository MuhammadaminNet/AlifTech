using AlifTechTask.Data.IRepositories;
using AlifTechTask.Domain.Enums;
using AlifTechTask.Domain.Models.Users;
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
        private readonly IRepository<User> _repository;
        public async Task<string> GenereToken(string login, string password)
        {
            var user = await _repository.GetAsync(
                u => u.Login == login && u.Password == password &&  u.State != ItemState.Deleted);

            if (user is null)
                throw new Exception("User not found");

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();




            byte[] tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);




            SecurityTokenDescriptor tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = _configuration["JWT:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
