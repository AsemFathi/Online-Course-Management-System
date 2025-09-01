using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Course_Management_System.Data;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;
using System.Threading.Tasks;

namespace Online_Course_Management_System.Pages.Admin.Students
{
    public class AddModel : PageModel
    {
        /*private readonly IStudentService _studentService;
        public AddModel(IStudentService studentService)
        {
            _studentService = studentService;
        }*/
        private readonly AppDbContext _context;
        private readonly IAuthServices _authServices;
        public AddModel(AppDbContext context, IAuthServices authServices)
        {
            _context = context;
            _authServices = authServices;
        }
        [BindProperty]
        public UserDto Student { get; set; } = new UserDto(); 
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost() 
        {
            await _authServices.RegisterAsync(Student);
                
            return RedirectToPage("/Admin/Students/Index");
        }

    }
}
