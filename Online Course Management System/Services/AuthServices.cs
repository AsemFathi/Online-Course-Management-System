using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Online_Course_Management_System.Data;
using Online_Course_Management_System.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Online_Course_Management_System.Services
{
    public class AuthServices(AppDbContext context , IConfiguration configuration) : IAuthServices
    {
        
        public string UserRole { get; set; }
        public Guid UserID { get; set; } 

        public async Task<TokenResponseDto?> LoginAsync(string username, string password)
        {
            var User = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (User == null) return null;
            if (User.Username != username) return null;
            if (new PasswordHasher<User>().VerifyHashedPassword(User, User.PasswordHashed, password) == PasswordVerificationResult.Failed)
                return null;
            TokenResponseDto response = await CreateTokenResponse(User);
            UserRole = User.Role;
            UserID = User.Id;
            return response;
        }

        private async Task<TokenResponseDto> CreateTokenResponse(User User)
        {
            return new TokenResponseDto
            {
                AccessToken = GetToken(User),
                RefreshToken = await GenerateAndSaveRefreshToken(User)
            };
        }

        public async Task<User?> RegisterAsync(UserDto request)
        {
            //check if username is used already 
            if(await context.Users.AnyAsync(u => u.Username == request.Username)) return null;

            var User = new User();
            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(User, request.Password);
            User.Username = request.Username;
            User.PasswordHashed = hashedPassword;
            User.Role = "Student";

            var student = new Student
            {
                Name = request.Name,
                Email = request.Email,
                UserId = User.Id,
                User = User
            };

            context.Users.Add(User);
            context.Students.Add(student);
            context.SaveChanges();
            return User;
        }

        public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId , request.RefreshToken);
            if (user == null) return null;

            return await CreateTokenResponse(user);
            
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshToken(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return refreshToken;
        }


        private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await context.Users.FindAsync(userId);
            if(user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow) 
                return null;

            return user;
        }


        public string GetToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var TokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(TokenDescriptor);
        }


        public async Task<bool> LogoutAsync(string refreshToken)
        {
            var token = await context.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if (token == null) return false;

            context.Users.Remove(token);
            context.SaveChanges();
            return true;
        }


    }
}
