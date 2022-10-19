using AlifTechTask.Domain.Enums;

namespace AlifTechTask.Domain.Commons
{
    public class Auditable
    {
        public Guid Id { get; set; }
        public ItemState State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
