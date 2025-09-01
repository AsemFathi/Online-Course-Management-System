using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Course_Management_System.Data;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;

namespace Online_Course_Management_System.Pages.Admin.Courses
{
    public class IndexModel : PageModel
    {
        /*private readonly IcourseService _icourseService;


        public IndexModel(IcourseService icourseService)
        {
            _icourseService = icourseService;
        }*/
        
        private readonly AppDbContext _appDbContext;
        public IndexModel(AppDbContext context)
        {
            _appDbContext = context;
        }
        public List<Course> Courses { get; set; } = new List<Course>();
        public void OnGet()
        {
            
            Courses = _appDbContext.Courses.
                Include(c => c.Enrollments)
                .ToList();

        }

        public IActionResult OnPostDelete(int id)
        {
            var course = _appDbContext.Courses.Find(id);
            if (course != null)
            {
                _appDbContext.Courses.Remove(course);
                _appDbContext.SaveChanges();
            }
            return RedirectToPage();
        }
    }
}
