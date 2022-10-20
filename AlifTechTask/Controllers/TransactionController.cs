using AlifTechTask.Service.DTOs.Transactions;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace AlifTechTask.Api.Controllers
{
    public class TransactionController : BaseController
    {
        [HttpPost]
        public async ValueTask<ActionResult<Transaction>> TopUpTheBalance(TransactionMoneyDto dto)
        {
            return null;
        }
    }
}
