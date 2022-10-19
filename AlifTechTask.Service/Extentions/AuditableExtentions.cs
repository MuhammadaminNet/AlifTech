using AlifTechTask.Domain.Commons;
using AlifTechTask.Domain.Enums;

namespace AlifTechTask.Service.Extentions
{
    public static class AuditableExtentions
    {
        /// <summary>
        /// Extention method for auditable class that gives a required values to some properties
        /// </summary>
        /// <param name="auditable"></param>
        public static void Create(this Auditable auditable) =>
            (auditable.CreatedAt, auditable.ItemState) = (DateTime.UtcNow, ItemState.Created);

        /// <summary>
        /// Extention method for auditable class that gives a required values to some properties
        /// </summary>
        /// <param name="auditable"></param>
        public static void Update(this Auditable auditable) =>
            (auditable.UpdatedAt, auditable.ItemState) = (DateTime.UtcNow, ItemState.Created);
    }
}
