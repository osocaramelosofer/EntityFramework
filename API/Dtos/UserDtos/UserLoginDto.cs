using System.ComponentModel.DataAnnotations;

namespace API.Dtos.UserDtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "The email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The password is requied")]
        public string Password { get; set; }

    }
}
