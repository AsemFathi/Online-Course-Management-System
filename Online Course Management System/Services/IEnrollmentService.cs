using Online_Course_Management_System.Models;

namespace Online_Course_Management_System.Services
{
    public interface IEnrollmentService
    {
        List<Enrollment> GetAllEnrollments();
        List<Enrollment> GetEnrollmentsByStudentId(int studentId);
        List<Enrollment> GetEnrollmentsByCourseId(int courseId);
        public void AddEnrollments(Enrollment enrollment);
        void AddEnrollment(int studentId, int courseId);
        void DeleteEnrollment(int enrollmentId);
    }
}
