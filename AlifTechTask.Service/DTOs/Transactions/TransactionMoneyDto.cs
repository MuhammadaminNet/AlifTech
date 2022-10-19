namespace AlifTechTask.Service.DTOs.Transactions
{
    public class TransactionMoneyDto
    {
        /// <summary>
        /// Id of sender
        /// </summary>
        public Guid From { get; set; }

        /// <summary>
        /// Id of achiever
        /// </summary>
        public Guid To { get; set; }

        /// <summary>
        /// Amount of transacted money
        /// </summary>
        public decimal Amount { get; set; }
    }
}
