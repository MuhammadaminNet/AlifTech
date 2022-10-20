using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlifTechTask.Api.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

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
            return Ok(await transactionService.CompleateBalanse(achieverPhone, amount, Guid.Parse(id.Value)));
        }

        /// <summary>
        /// Get all history of actions performed in this month by entered phone 
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("expenses")]
        public async ValueTask<ActionResult<Transaction>> GetAllOperationsPerformedOfCurrentMonth(string phone)
            => Ok(await transactionService.GetAllOperationsPerformedOfCurrentMonth(phone));

        /// <summary>
        /// For know current balance of e-wellet
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("check-balance")]
        public async ValueTask<ActionResult<decimal>> CheckInfoAboutBalance(string phone)
            => Ok(await transactionService.GetBalance(phone));
    }
}
