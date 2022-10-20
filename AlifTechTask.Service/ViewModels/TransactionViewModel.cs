using AlifTechTask.Domain.Models.Transactions;

namespace AlifTechTask.Service.DTOs.Transactions
{
    public class TransactionViewModel
    {
        public int CountOfOperations { get; set; }
        public decimal SummOfSentSomoni { get; set; } = new decimal(0);
        public decimal SummOfAchievedSomoni { get; set; } = new decimal(0);
        public virtual IEnumerable<Transaction> Transactions { get; set; }
    }
}
