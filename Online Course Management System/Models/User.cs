using Microsoft.EntityFrameworkCore;

namespace Online_Course_Management_System.Models
{
    public class User
    {
       
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHashed { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public Student? Student { get; set; }
    }
}
