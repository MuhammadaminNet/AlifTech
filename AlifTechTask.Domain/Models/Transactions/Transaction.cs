using AlifTechTask.Domain.Commons;
using AlifTechTask.Domain.Models.Users;

namespace AlifTechTask.Domain.Models.Transactions
{
    public class Transaction : Auditable
    {
        public decimal Amount { get; set; }

        public Guid SenderId { get; set; }
        public User Sender { get; set; }

        public Guid AchieverId { get; set; }
        public User Achiever { get; set; }
    }
}
