using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlifTechTask.Api.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;

        public TransactionController(ITransactionService transactionService, IUserService userService)
            => (this._transactionService, this._userService) = (transactionService, userService);
        
        
        /// <summary>
        /// Checking a account in entered phone is exist or not. true if account exist, otherwise false 
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async ValueTask<ActionResult<IEnumerable<bool>>> CheckAccountAsync(string phone)
            => Ok(await _userService.GetAllAsync(u => u.Phone == phone) != null );


        /// <summary>
        /// Filling balance of some one
        /// </summary>
        /// <param name="achieverPhone"></param>
        /// <param name="amount"></param>
        /// <returns>Transaction</returns>
        [Authorize]
        [HttpPost("fill-balance")]
        public async ValueTask<ActionResult<Transaction>> TopUpTheBalance(string achieverPhone, decimal amount)
        {
            var id = User.Claims.FirstOrDefault(u => u.Type.ToString().Equals("Id", StringComparison.InvariantCultureIgnoreCase));
            return Ok(await _transactionService.CompleateBalanse(achieverPhone, amount, Guid.Parse(id.Value)));
        }

        /// <summary>
        /// Get all history of actions performed in this month by entered phone 
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("expenses")]
        public async ValueTask<ActionResult<Transaction>> GetAllOperationsPerformedOfCurrentMonth(string phone)
            => Ok(await _transactionService.GetAllOperationsPerformedOfCurrentMonth(phone));

        /// <summary>
        /// For know current balance of e-wellet
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("check-balance")]
        public async ValueTask<ActionResult<User>> CheckInfoAboutBalance(string phone)
            => Ok(await _transactionService.GetBalance(phone));
    }
}
