using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AlifTechTask.Service.DTOs
{
    public class UserForCreationDto
    {
        [Required]
        [MinLength(3)]
        [DefaultValue("Unknown")]
        public string FirstName { get; set; }


        [Required]
        [MinLength(3)]
        [DefaultValue("Unknown")]
        public string LastName { get; set; }


        [Required]
        [MinLength(6)]
        [NotNull]
        public string Login { get; set; }


        [Required]
        [MinLength(6)]
        [NotNull]
        public string Password { get; set; }


        [DefaultValue(false)]
        public bool IsIdentified { get; set; }
    }
}
