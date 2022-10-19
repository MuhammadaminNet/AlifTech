using System.ComponentModel;

namespace AlifTechTask.Service.DTOs.Transactions
{
    public class ReplenishmentsDto
    {
        /// <summary>
        /// Total count of replenishmets in current month
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Total emount of echieved money
        /// </summary>
        [DefaultValue(0.0)]
        public decimal AmountEchieved { get; set; }

        /// <summary>
        /// Total emount of sent money
        /// </summary>
        [DefaultValue(0.0)]
        public decimal AmountSent { get; set; }

        /// <summary>
        /// All transactions
        /// </summary>
        public ICollection<TransactionViewModel> Transactions { get; set; }
    }
}
