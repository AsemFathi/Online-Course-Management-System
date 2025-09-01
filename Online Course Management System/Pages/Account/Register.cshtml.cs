using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace Online_Course_Management_System.Pages.Account
{
    public class RegisterModel : PageModel
    {
     
        private readonly IAuthServices _authServices;
        public RegisterModel(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [BindProperty]
        public UserDto User { get; set; }

        [TempData]
        public string ToastMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            
            var res = await _authServices.RegisterAsync(User);
            if (res is null)
            {
                ToastMessage = "Registration failed: username is already existed!";
                return Page();
            }

            ToastMessage = "Registration Successfull!";
            return RedirectToPage("/Index");
        }
    }
}

