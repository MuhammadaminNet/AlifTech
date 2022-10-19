using AlifTechTask.Domain.Commons;

namespace AlifTechTask.Domain.Models
{
    public class User : Auditable
    {
        public string? FullName { get; set; } = "Client";
        public string Email { get; set; }
        public decimal CurrentBalance { get; set; } = decimal.Zero;
        public decimal TotalBalanceOfCurrentMonth { get; set; } = decimal.Zero;
    }
}
