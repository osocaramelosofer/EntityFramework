using API.Data;
using API.Dtos.IdentityUser;
using API.Dtos.UserDtos;
using API.Models;
using API.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace API.Repository.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        private string secretKey;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserRepository(DatabaseContext context, IConfiguration config, RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            secretKey = config.GetValue<string>("ApiSettings:secret");
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }
        public AppUser GetUser(string userId)
        {
            return _context.AppUser.FirstOrDefault(u => u.Id == userId);
        }

        public ICollection<AppUser> GetUsers()
        {
            return _context.AppUser.OrderBy(u => u.UserName).ToList();
        }

        public bool isUniqueUser(int userId)
        {
            throw new NotImplementedException();
        }

        public bool isUniqueUser(string email)
        {
            var user = _context.AppUser.FirstOrDefault(u => u.UserName.ToLower().Trim() == email.ToLower().Trim());
            if ( user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            //var passwordEncrypted = getmd5(userLoginDto.PWD);
            var user = _context.AppUser.FirstOrDefault(
                u => u.UserName.ToLower() == userLoginDto.Email.ToLower());

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

            // if there is no user with that email linked
            if (user == null)
            {
                return new UserLoginResponseDto()
                {
                    Token = "",
                    User = null,
                    LoginErrorMessage = "It seems we don't have any account linked with that email address."
                };
            }

            if (!isPasswordValid)
            {
                return new UserLoginResponseDto()
                {
                    Token = "",
                    User = null,
                    LoginErrorMessage = "It seems the password is wrong"
                };
            }

            // if user exists
            var roles = await _userManager.GetRolesAsync(user);
            var handleToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            // we describe how we want out token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires=DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // we create the token using the token description of above
            var token = handleToken.CreateToken(tokenDescriptor);

            UserLoginResponseDto userLoginResponseDto = new UserLoginResponseDto()
            {
                Token = handleToken.WriteToken(token),
                User = _mapper.Map<AppUserDto>(user)
            };
            
            return userLoginResponseDto;
        }

        public async Task<AppUserDto> Registry(AppUserCreateDto appUserCreateDto)
        {

            AppUser user = new AppUser()
            {
                UserName = appUserCreateDto.Email,
                Email = appUserCreateDto.Email,
                NormalizedEmail = appUserCreateDto.Email.ToUpper()
            };

            var result = await _userManager.CreateAsync(user, appUserCreateDto.Password);

            if (result.Succeeded)
            {
                // Just for the firrst time and create the roles
                if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                    await _roleManager.CreateAsync(new IdentityRole("dev"));
                    await _roleManager.CreateAsync(new IdentityRole("user"));
                }

                await _userManager.AddToRoleAsync(user, "admin");
                var userCreated = _context.AppUser.FirstOrDefault(user => user.Email == appUserCreateDto.Email);

                return _mapper.Map<AppUserDto>(userCreated);

            }
            return new AppUserDto();
        }

    }
}

