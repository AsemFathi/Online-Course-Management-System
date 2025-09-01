using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Course_Management_System.Data;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;

namespace Online_Course_Management_System.Pages.Admin.Courses
{
    public class AddModel : PageModel
    {
        /*private readonly IcourseService _courseService;
        public AddModel(IcourseService icourseService)
        {
            _courseService = icourseService;
        }*/

        private readonly AppDbContext _context;
        public AddModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Course> Courses { get; set; } = new List<Course>();

        [BindProperty]
        public Course NewCourse { get; set; } = new Course();
        public void OnGet()
        {
            //Courses = _courseService.GetAllCourses();
            Courses = _context.Courses.ToList();
            NewCourse.StartDate = DateTime.Now;
            NewCourse.EndDate = DateTime.Now;
        }

        public IActionResult OnPost() 
        {
            if (NewCourse != null)
            {
                //_courseService.AddCourse(NewCourse);
               
                    _context.Courses.Add(NewCourse);
                    _context.SaveChanges();
                

                return RedirectToPage("/Admin/Courses/Index");
            }

            return RedirectToPage("/Admin/Courses/Index");
        }
    }
}
