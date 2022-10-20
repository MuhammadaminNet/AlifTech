using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.DTOs.Users;
using AlifTechTask.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlifTechTask.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
            => this._userService = userService;


        /// <summary>
        /// Api for create user
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<ActionResult<User>> CreateAsync(string phone, string password)
            => Ok(await _userService.CreateAsync(phone, password));


        /// <summary>
        /// Select all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<User>>> GetAllAsync()
            => Ok(await _userService.GetAllAsync());


        /// <summary>
        /// Identify user 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("{identify-user}")]
        public async ValueTask<ActionResult<User>> IdentifyUser(string phone, string password, UserForIdentifyDto dto)
            => Ok(await _userService.UpdateAsync(u => u.Phone == phone && u.Password == password, dto));
    }
}
