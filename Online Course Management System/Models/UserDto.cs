using System.ComponentModel.DataAnnotations;

namespace Online_Course_Management_System.Models
{
    public class UserDto
    {
        [Required(ErrorMessage = "Username must be filled")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password must be filled")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full name must be filled")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email must be filled")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
    }
}
