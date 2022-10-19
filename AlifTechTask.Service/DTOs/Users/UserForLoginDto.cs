using System.ComponentModel.DataAnnotations;

namespace AlifTechTask.Service.DTOs.Users
{
    public class UserForLoginDto
    {
        /// <summary>
        /// Phone for knowing user is exist or not
        /// </summary>
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// Password for security
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
