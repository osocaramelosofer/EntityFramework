using System.ComponentModel.DataAnnotations;

namespace API.Dtos.UserDtos
{
    public class AppUserCreateDto
    {
        public string Email { get; set; }
        public string Password { get; set; }


    }
}
