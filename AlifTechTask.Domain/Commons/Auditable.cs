using AlifTechTask.Domain.Enums;

namespace AlifTechTask.Domain.Commons
{
    public class Auditable
    {
        public Guid Id { get; set; }
        public ItemState ItemState { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
