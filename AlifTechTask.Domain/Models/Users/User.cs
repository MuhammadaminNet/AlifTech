using AlifTechTask.Domain.Commons;
using AlifTechTask.Domain.Enums;
using Newtonsoft.Json;

namespace AlifTechTask.Domain.Models.Users
{
    public class User : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public decimal Balance { get; set; } = new decimal(0);

        [JsonIgnore]
        public string Password { get; set; }
        public bool IsIdentified { get; set; }
        public ItemState State { get; set; } = ItemState.Created;
    }
}
