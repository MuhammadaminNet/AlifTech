using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.DTOs.Users;
using AlifTechTask.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlifTechTask.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        /// <summary>
        /// Api for create user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<ActionResult<User>> CreateAsync(string phone, string password)
            => Ok(await userService.CreateAsync(phone, password));


        /// <summary>
        /// Api for delete user who owns entered phone
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpDelete]
        public async ValueTask<ActionResult<bool>> DeleteAsync(string phone, string password)
            => Ok(await userService.DeleteAsync(u => u.Phone == phone && u.Password == password));


        /// <summary>
        /// Api for update user by entered phone to entered dto
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async ValueTask<ActionResult<User>> UpdateAsync(string phone, string password, UserForIdentifyDto dto)
            => Ok(await userService.UpdateAsync(u => u.Phone == phone, dto));


        /// <summary>
        /// Api for select one user by entered phone
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpGet("{phone}")]
        public async ValueTask<ActionResult<User>> GetAsync(string phone)
            => Ok(await userService.GetAsync(u => u.Phone == phone));


        /// <summary>
        /// Select all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<User>>> GetAllAsync()
            => Ok(await userService.GetAllAsync());


        /// <summary>
        /// Identify user 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{identify-user}")]
        public async ValueTask<ActionResult<User>> IdentifyUser(string phone, string password, UserForIdentifyDto dto)
            => Ok(await userService.UpdateAsync(u => u.Phone == phone && u.Password == password, dto));
    }
}
