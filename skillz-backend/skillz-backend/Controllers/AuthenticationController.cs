using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skillz_backend.data;
using skillz_backend.DTOs;
using skillz_backend.models;
using skillz_backend.Services;

namespace skillz_backend.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly SkillzDbContext _skillzDbContext;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        // Constructor to inject dependencies
        public AuthenticationController(SkillzDbContext skillzDbContext, IAuthenticationService authenticationService)
        {
           _skillzDbContext = skillzDbContext;
           _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        // Registers a new user
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            // Checks if the email is already taken
            if (await UserExists(registerDto.Email)) return BadRequest("Email is taken");

            // Generates a unique key for the user's password
            using var hmac = new HMACSHA512();

            // Creates a new user based on the provided registration data
            var user = new User
            {
                Username = registerDto.Username.ToLower(),
                Email = registerDto.Email.ToLower(),
                PhoneNumber = registerDto.PhoneNumber.ToLower(),
                Location = registerDto.Location.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            // Adds the user to the database and saves changes
            _skillzDbContext.Users.Add(user);
            await _skillzDbContext.SaveChangesAsync();

            // Generates a token for the newly registered user
            return new UserDto
            {   
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email, // Set the email property
                Token = _authenticationService.GenerateToken(user)
            };
        }

        // Logs in an existing user
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            // Retrieves the user based on the provided username
            var user = await _skillzDbContext.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email);

            // If the user does not exist, return Unauthorized
            if (user == null) return Unauthorized("Invalid email");

            // Creates a hash using the user's password salt
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            // Compares the computed hash with the stored password hash
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            // If the password is valid, generate a token for the user
            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email, // Set the email property
                Token = _authenticationService.GenerateToken(user)
            };
        }

        // Checks if a user with the given username already exists
        private async Task<bool> UserExists(string username)
        {
            return await _skillzDbContext.Users.AnyAsync(x => x.Username == username.ToLower());
        }
    }
}
