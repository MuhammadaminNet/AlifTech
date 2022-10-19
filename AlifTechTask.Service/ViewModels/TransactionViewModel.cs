namespace AlifTechTask.Service.DTOs.Transactions
{
    public class TransactionViewModel
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
        /// The amount of transacted money 
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Date of action
        /// </summary>
        public DateTime ImplementationDate { get; set; }
    }
}
