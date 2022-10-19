using AlifTechTask.Domain.Commons;

namespace AlifTechTask.Domain.Models.Users
{
    public class Wallet : Auditable
    {
        public decimal Balance { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
