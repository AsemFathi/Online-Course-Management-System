using Online_Course_Management_System.Models;

namespace Online_Course_Management_System.Services
{
    public interface IcourseService
    {
        public List<Course> GetAllCourses();
        public Course GetCourseById(int id);
        public Course GetCourseByTitle(string title);
        public void EditCourses(Course course);
        public void DeleteCourses(int id);
        public void AddCourse(Course course);
        
    }
}
