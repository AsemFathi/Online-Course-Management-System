using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Online_Course_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServices authServices) : ControllerBase
    {
        
        
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authServices.RegisterAsync(request);
            if (user == null) return BadRequest("username is already exist");

            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string username , string password)
        {
            var token = await authServices.LoginAsync(username , password);
            if (token == null) return BadRequest("Invalid username or password ");



            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authServices.RefreshTokensAsync(request);
            if (result == null || result.AccessToken == null || result.RefreshToken == null) 
                return Unauthorized(result);
            return Ok(result);
        }


        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {

            return Ok("You are authenticated!");
        }
        [Authorize(Roles="Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {

            return Ok("You are admin!");
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDto request)
        {
            var result = await authServices.LogoutAsync(request.RefreshToken);
            if (!result) return BadRequest("Invalid refresh token");

            return Ok("Logged out successfully");
        }

    }
}
