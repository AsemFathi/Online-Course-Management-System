using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Course_Management_System.Data;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;

namespace Online_Course_Management_System.Pages.Admin.Enrollments
{
    public class IndexModel : PageModel
    {
        /*private readonly IEnrollmentService _enrollmentService;
        public IndexModel(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }*/

        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public void OnGet()
        {
            //Enrollments = _enrollmentService.GetAllEnrollments();   
            Enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToList();
        }
        public IActionResult OnPostDelete(int id)
        {
            //_enrollmentService.DeleteEnrollment(id);
            var enrollment = _context.Enrollments.Find(id);
            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();
            return RedirectToPage();
        }
    }
}
