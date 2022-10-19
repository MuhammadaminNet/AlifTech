using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AlifTechTask.Service.DTOs.Transactions
{
    public class TransactionMoneyDto
    {
        /// <summary>
        /// Id of sender
        /// </summary>
        [Required]
        [NotNull]
        public Guid From { get; set; }

        /// <summary>
        /// Id of achiever
        /// </summary>
        [Required]
        [NotNull]
        public Guid To { get; set; }

        /// <summary>
        /// Amount of transacted money
        /// </summary>
        [Required]
        public decimal Amount { get; set; }
    }
}
