using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AlifTechTask.Service.DTOs.Transactions
{
    public class TransactionMoneyDto
    {
        /// <summary>
        /// Phone of achiever
        /// </summary>
        [Required]
        [MinLength(9)]
        [NotNull]
        public string Phone { get; set; }

        /// <summary>
        /// Amount of transacted money
        /// </summary>
        [Required]
        public decimal Amount { get; set; }
    }
}
