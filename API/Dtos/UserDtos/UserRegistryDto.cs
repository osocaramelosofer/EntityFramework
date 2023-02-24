using System.ComponentModel.DataAnnotations;

namespace API.Dtos.UserDtos
{
    public class UserRegistryDto
    {
        [Required(ErrorMessage = "The displayname is required")]

        public string DISPLAY_NAME { get; set; }
        [Required(ErrorMessage = "The password is requied")]
        public string PWD { get; set; }
        public string ROLID { get; set; }
        [Required(ErrorMessage ="The user is required")]
        public string MAIL { get; set; }
        public Boolean ENABLED { get; set; }
    }
}
