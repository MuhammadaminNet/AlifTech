using AlifTechTask.Domain.Commons;

namespace AlifTechTask.Domain.Models
{
    public class Order : Auditable
    {
        public Guid UserId { get; set; }
        public decimal Summ { get; set; }
    }
}
