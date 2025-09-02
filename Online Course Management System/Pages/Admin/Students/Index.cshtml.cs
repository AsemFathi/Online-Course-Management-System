
//Code without database
/*using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;

namespace Online_Course_Management_System.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly IStudentService _studentService;
        private readonly IEnrollmentService _enrollmentService;
        public IndexModel(IStudentService studentService, IEnrollmentService enrollmentService)
        {
            _studentService = studentService;
            _enrollmentService = enrollmentService;
        }

        public List<Student> Students { get; set; } = new List<Student>();
        public List<Enrollment> Enrollments { get; set; }= new List<Enrollment>();

        public void OnGet()
        {
            Students = _studentService.GetAllStudents();
            Enrollments = _enrollmentService.GetAllEnrollments();
        }

        public IActionResult OnPostDelete(int id)
        {

            _studentService.DeleteStudent(id);
            return RedirectToPage();
        }
    }
}
*/

//code using MSSQL Database
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Course_Management_System.Data;
using Online_Course_Management_System.Models;

namespace Online_Course_Management_System.Pages.Admin.Students
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Student> Students { get; set; } = new List<Student>();
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public string SearchTerm { get; set; }

        
        
        public async Task OnGetAsync(string searchTerm)
        {

            Students = _context.Students
                               .Include(s => s.Enrollments)
                               .ToList();

            Enrollments = _context.Enrollments
                                  .Include(e => e.Student)
                                  .Include(e => e.Course)
                                  .ToList();

            SearchTerm = searchTerm;
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Students = Students.Where(u => u.Name.Contains(SearchTerm) || u.Email.Contains(SearchTerm)).ToList();
            }
            
            
        }

        public IActionResult OnPostDelete(int id)
        {


            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return RedirectToPage();
        }
        

    }
}
