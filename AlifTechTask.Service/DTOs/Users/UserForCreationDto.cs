using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AlifTechTask.Service.DTOs.Users
{
    public class UserForCreationDto
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

        /// <summary>
        /// Unique information of user
        /// </summary>
        [Required]
        [MinLength(9)]
        [NotNull]
        public string Phone { get; set; }

        /// <summary>
        /// Security key for user
        /// </summary>
        [Required]
        [MinLength(6)]
        [NotNull]
        public string Password { get; set; }

        /// <summary>
        /// Info about user: true = this is idendified user, false = this is undefined
        /// </summary>
        [DefaultValue(false)]
        public bool IsIdentified { get; set; }
    }
}
