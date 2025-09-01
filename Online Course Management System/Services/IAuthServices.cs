using Online_Course_Management_System.Models;

namespace Online_Course_Management_System.Services
{
    public interface IAuthServices
    {
        public string UserRole { get; set; }
        public Guid UserID { get; set; }
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(string username, string password);

        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
        Task<bool> LogoutAsync(string refreshToken);

    }
}
