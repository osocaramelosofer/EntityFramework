using API.Dtos;
using API.Dtos.IdentityUser;
using API.Dtos.UserDtos;
using API.Repository.Classes;
using API.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        protected ApiResponse _apiResponse;

        public UserController(IUserRepository user, IMapper mapper)
        {
            _userRepo = user;
            _mapper = mapper;
            _apiResponse = new ApiResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSysUsers()
        {
            var listUsers = _userRepo.GetUsers();
            var listUsersDto = new List<AppUserDto>();
            foreach (var user in listUsers)
            {
                listUsersDto.Add(_mapper.Map<AppUserDto>(user));
            }
            return Ok(listUsersDto);
        }


        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(string id)
        {
            var user = _userRepo.GetUser(id);
            var userDto = _mapper.Map<AppUserDto>(user);
            return userDto == null ? NotFound() : Ok(user);

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] AppUserCreateDto userRegistry)
        {
            bool isUniqueUser = _userRepo.isUniqueUser(userRegistry.Email);

            if (isUniqueUser == false)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("The email already exists");

                return BadRequest(_apiResponse);
            }
            var user = await _userRepo.Registry(userRegistry);
            if (user == null)
            {
                _apiResponse.StatusCode=HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Something went wrong on the registry");

                return BadRequest(_apiResponse);
            }

            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            _apiResponse.SuccessResponseMessage = "The user was created successfully";

            return Ok(_apiResponse);
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userLogin)
        {
            var loginResponse = await _userRepo.Login(userLogin);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add(loginResponse.LoginErrorMessage);

                return BadRequest(_apiResponse);
            }
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            _apiResponse.Result = loginResponse;
            return Ok(_apiResponse);
        }
    }
}
