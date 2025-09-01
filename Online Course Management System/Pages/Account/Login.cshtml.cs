using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;

namespace Online_Course_Management_System.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAuthServices _authServices;
        public LoginModel(IAuthServices authServices)
        {
            _authServices = authServices;
        }
        [BindProperty]
        public UserDto User { get; set; }
        public string ToastMessage { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _authServices.LoginAsync(User.Username , User.Password);
            if (user == null)
            {
                ToastMessage = "incorrect Username or Password";
                return Page();
            }

            if (_authServices.UserRole == "Student") 
                return RedirectToPage("/StudentPages/StudentDashboard", new { Username = User.Username , UserId = _authServices.UserID} );

            return RedirectToPage("/Admin/Index");
            
        }
    }
}
