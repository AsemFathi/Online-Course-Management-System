using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Course_Management_System.Data;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;

namespace Online_Course_Management_System.Pages.Admin.Courses
{
    public class EditModel : PageModel
    {
        /*private readonly IcourseService _icourseService;

        public EditModel(IcourseService icourseService)
        {
            _icourseService = icourseService;
        }*/

        private readonly AppDbContext _context;
        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Course> Courses { get; set; } = new List<Course>();
        [BindProperty]
        public Course Course { get; set; } = new Course();

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public IActionResult OnGet()
        {
            //Course = _icourseService.GetCourseById(Id);
            Course = _context.Courses.FirstOrDefault(c => c.Id == Id);
            return Page();
        }

        public IActionResult OnPost()
        {
            //_icourseService.EditCourses(Course);
            _context.Courses.Update(Course);
            _context.SaveChanges();
            return RedirectToPage("/Admin/Courses/Index");
        }
    }
}
