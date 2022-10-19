using AlifTechTask.Domain.Commons;
using AlifTechTask.Domain.Models.Users;

namespace AlifTechTask.Domain.Models.Transactions
{
    public class Transaction : Auditable
    {
        public decimal Amount { get; set; }

        public Guid SenderId { get; set; }
        public Wallet SenderWallet { get; set; }

        public Guid ReceiverId { get; set; }
        public Wallet ReceiverWallet { get; set; }
    }
}
