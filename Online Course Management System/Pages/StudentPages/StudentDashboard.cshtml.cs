using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Course_Management_System.Data;
using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;

namespace Online_Course_Management_System.Pages.StudentPages
{
    public class StudentDashboardModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAuthServices _authServices;

        public StudentDashboardModel(AppDbContext appDbContext, IAuthServices authServices)
        {
            _appDbContext = appDbContext;
            _authServices = authServices;
        }
        public string Username { get; set; }

        public Student Student { get; set; }
        public List<Course> SuggestedCourses { get; set; } = new List<Course>();
        
        public IActionResult OnGet(string username , Guid UserId)
        {
            Username = username;
            Student = _appDbContext.Students
                .Include(s=>s.Enrollments)
                    .ThenInclude(e => e.Course)
                .FirstOrDefault(s => s.UserId == UserId);

            var EnrolledCourses = Student?.Enrollments?.Select(s => s.Course).ToList()?? new List<Course>();
            //var EnrollmentsCourses = Student.Enrollments;
            var AllCourses = _appDbContext.Courses.ToList();

            SuggestedCourses = AllCourses
                .Where(c => !EnrolledCourses.Any(ec => ec.Id == c.Id))
                    .ToList();


            return Page();
        }

        public IActionResult OnPostEnroll(Guid userId , int courseId)
        {
            var enrollment = new Enrollment
            {
                CourseId = courseId,
                StudentId = _appDbContext.Students.First(s => s.UserId == userId).Id,
                EnrollmentDate = DateTime.Now
            };

            _appDbContext.Enrollments .Add(enrollment);
            _appDbContext.SaveChanges();
            return RedirectToPage("/StudentPages/StudentDashboard", new { username = Username , UserId = userId});
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            var studentUserId = Student?.UserId;
            // get refresh token from cookie/session
            var user = await _appDbContext.Users.FirstOrDefaultAsync(s => s.Id == studentUserId);
            var refreshToken = user?.RefreshToken;

            if (string.IsNullOrEmpty(refreshToken))
            {
                // clear session and redirect to login
                
                return RedirectToPage("/Account/Login");
            }

            // Call logout service directly (no HttpClient needed)
            var result = await _authServices.LogoutAsync(refreshToken);

            // clear session/cookie anyway
            

            return RedirectToPage("/Account/Login");
        }
    }
}
