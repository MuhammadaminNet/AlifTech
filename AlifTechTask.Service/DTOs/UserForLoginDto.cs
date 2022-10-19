using System.ComponentModel.DataAnnotations;

namespace AlifTechTask.Service.DTOs
{
    public class UserForLoginDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
