using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Course_Management_System.Data;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;

namespace Online_Course_Management_System.Pages.Admin.Courses
{
    public class CourseDetailsModel : PageModel
    {
        /*private readonly IcourseService _icourseService;

        public CourseDetailsModel(IcourseService icourseService)
        {
            _icourseService = icourseService;
        }*/
        private readonly AppDbContext _appDbContext;
        public CourseDetailsModel(AppDbContext context)
        {
            _appDbContext = context;
        }
        public Course Course { get; set; } = new Course();
        public List<Enrollment> Enrollments { get; set; } = new();

        public IActionResult OnGet(string title)
        {
            //Course = _icourseService.GetCourseByTitle(title);

            Course = _appDbContext.Courses
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
                .FirstOrDefault(c => c.Title == title);

            Enrollments = Course.Enrollments.ToList(); 
            return Page();
        }
        public IActionResult OnPostDelete(int id)
        {
            //_icourseService.DeleteCourses(id);

            var course = _appDbContext.Courses.Find(id);
            if (course != null)
            {
                _appDbContext.Courses.Remove(course);
                _appDbContext.SaveChanges();
            }
            return RedirectToPage("/Admin/Courses/Index");
        }
    }
}
