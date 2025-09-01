using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Course_Management_System.Data;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;

namespace Online_Course_Management_System.Pages.Admin.Enrollments
{
    public class CreateModel : PageModel
    {
        /*private readonly IEnrollmentService _enrollmentService;
        private readonly IStudentService _studentService;
        private readonly IcourseService _courseService;
        public CreateModel(IEnrollmentService enrollmentService, IStudentService studentService, IcourseService icourseService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = icourseService;
        }
        */

        private readonly AppDbContext _context;
        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrollment Enrollment { get; set; } = new Enrollment();

        public SelectList StudentList { get; set; }
        public SelectList CourseList { get; set; }

        public void OnGet()
        {
            //StudentList = new SelectList(_studentService.GetAllStudents(),"Id","Name");
            //CourseList = new SelectList(_courseService.GetAllCourses(),"Id","Title");
            StudentList = new SelectList(_context.Students.ToList(),"Id", "Name");
            CourseList = new SelectList(_context.Courses.ToList(), "Id", "Title");
            Enrollment.EnrollmentDate = DateTime.Now;
        }

        public IActionResult OnPost()
        {
            if(Enrollment != null)
            { 
                //_enrollmentService.AddEnrollments(Enrollment);
                _context.Enrollments.Add(Enrollment);
                _context.SaveChanges();
            }

            return RedirectToPage("/Admin/Enrollments/Index");
        }
    }
}
