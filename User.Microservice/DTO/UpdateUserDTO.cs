using System.ComponentModel.DataAnnotations;

namespace User.Microservice.DTO
{
    public class UpdateUserDTO
    {

        [Required]
        [MaxLength(10)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string OldPassword { get; set; }
    }
}
