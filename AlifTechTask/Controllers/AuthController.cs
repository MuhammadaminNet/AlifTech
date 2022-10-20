using AlifTechTask.Service.DTOs.Users;
using AlifTechTask.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlifTechTask.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
            => this._authService = authService;

        /// <summary>
        /// Authorization
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto dto)
            => Ok(await _authService.GenerateToken(dto));
    }
}
