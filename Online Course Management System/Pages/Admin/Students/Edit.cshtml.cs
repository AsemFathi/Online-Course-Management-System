using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Course_Management_System.Data;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;
using System.Threading.Tasks;

namespace Online_Course_Management_System.Pages.Admin.Students
{
    public class EditModel : PageModel
    {
        /*private readonly IStudentService _studentService;
        public EditModel(IStudentService studentService)
        {
            _studentService = studentService;
        }*/

        private readonly AppDbContext _appDbContext;
        public EditModel(AppDbContext context)
        {
            _appDbContext = context;
        }

        [BindProperty]
        public User User { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }


        public async Task<IActionResult> OnGet()
        {
            User = await _appDbContext.Users
                .Include(u => u.Student)
                .FirstOrDefaultAsync(u => u.Id == Id);
            return Page();
        }

        public IActionResult OnPost()
        {
            var existingUser = _appDbContext.Users
        .Include(u => u.Student)
        .FirstOrDefault(u => u.Id == User.Id);

            if (existingUser != null)
            {
                // Update User fields
                existingUser.Username = User.Username;
                existingUser.Student.Email = User.Student.Email;
                existingUser.Student.Name = User.Student.Name;

                // Update Student fields if needed
                if (existingUser.Student != null && User.Student != null)
                {
                    existingUser.Student.Name = User.Student.Name;
                    existingUser.Student.Email = User.Student.Email;
                }

                _appDbContext.SaveChanges();
            }

            return RedirectToPage("/Admin/Students/Index");
        }
    }
}
