using API.Dtos.IdentityUser;
using API.Models;

namespace API.Dtos.UserDtos
{
    public class UserLoginResponseDto
    {
        public AppUserDto User { get; set; }
        public string Token { get; set; }
        public string LoginErrorMessage { get; set; }
    }
}
