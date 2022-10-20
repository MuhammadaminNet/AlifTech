using AlifTechTask.Domain.Models.Transactions;

namespace AlifTechTask.Service.DTOs.Transactions
{
    public class TransactionViewModel
    {
        public decimal SummOfSentSomoni { get; set; }
        public decimal SummOfAchievedSomoni { get; set; }
        public virtual IEnumerable<Transaction> Transactions { get; set; }
    }
}
