using System.ComponentModel.DataAnnotations;

namespace AlifTechTask.Service.DTOs.Users
{
    public class UserForIdentifyDto
    {
        /// <summary>
        /// Firstname of user
        /// </summary>
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        /// <summary>
        /// Lastname of user
        /// </summary>
        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
    }
}
