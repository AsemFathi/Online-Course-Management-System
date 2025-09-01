using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Course_Management_System.Data;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;

namespace Online_Course_Management_System.Pages.Admin.Students
{
    public class StudentDetailsModel : PageModel
    {
        /*private readonly IStudentService _istudentService;
        public StudentDetailsModel(IStudentService studentService)
        {
            _istudentService = studentService;
        }*/

        private readonly AppDbContext _appDbContext;
        public StudentDetailsModel(AppDbContext context)
        {
            _appDbContext = context;
        }
        public Student Student { get; set; }


        public IActionResult OnGet(int id)
        {
            Student = _appDbContext.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefault(s => s.Id == id)?? new Student();

            return Page();
        }
    }
}
