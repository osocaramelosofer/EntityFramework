using API.Dtos.IdentityUser;
using API.Dtos.UserDtos;
using API.Models;

namespace API.Repository.Interfaces
{
    public interface IUserRepository
    {
        ICollection<AppUser> GetUsers();
        AppUser GetUser(string userId);
        bool isUniqueUser(int userId);
        bool isUniqueUser(string email);
        Task<UserLoginResponseDto> Login(UserLoginDto usuerLoginDto);
        Task<AppUserDto> Registry(AppUserCreateDto appUserCreatedDto);

    }
}
