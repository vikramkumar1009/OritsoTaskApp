using Microsoft.AspNetCore.Http;

namespace OritsoTaskApp.Models
{
    public class UserSignupDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
