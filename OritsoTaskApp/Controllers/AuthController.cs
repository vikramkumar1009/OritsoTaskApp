using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Services;
using OritsoTaskApp.Models;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromForm] UserSignupDto userDto)
        {
            if (_context.Users.Any(u => u.Email == userDto.Email))
                return BadRequest("Email already exists.");

            string imageFileName = null;

            if (userDto.ProfileImage != null && userDto.ProfileImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                imageFileName = $"{Guid.NewGuid()}_{userDto.ProfileImage.FileName}";
                var filePath = Path.Combine(uploadsFolder, imageFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await userDto.ProfileImage.CopyToAsync(stream);
                }
            }

            var user = new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                ProfileImage = imageFileName
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginData)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == loginData.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginData.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            var token = _jwtService.GenerateToken(user.Id, user.Email, user.FullName);
            return Ok(new { token, profileImage = user.ProfileImage });
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("User not found.");

            var userDetails = new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.ProfileImage
            };

            return Ok(userDetails);
        }

        [Authorize]
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.Select(u => new
            {
                u.Id,
                u.FullName,
                u.Email,
                u.ProfileImage
            }).ToList();

            return Ok(users);
        }
    }
}
