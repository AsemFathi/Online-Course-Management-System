using Online_Course_Management_System.Models;

namespace Online_Course_Management_System.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly List<Enrollment> Enrollments = new List<Enrollment>();
        private readonly IStudentService _studentService;
        private readonly IcourseService _courseService;
        private int _nextId = 1;

        public EnrollmentService(IStudentService studentService, IcourseService icourseService)
        {
            _courseService = icourseService;
            _studentService = studentService;

            AddEnrollment(1, 1); // Student 1 enrolled in Course 1
            AddEnrollment(2, 1); // Student 2 enrolled in Course 1
            AddEnrollment(3, 2); // Student 3 enrolled in Course 2
            AddEnrollment(1, 2); // Student 1 enrolled in Course 2
            AddEnrollment(4, 1); // Student 4 enrolled in Course 1
        }
        public void AddEnrollment(int studentId, int courseId)
        {
            var student = _studentService.GetStudentById(studentId);
            var course = _courseService.GetCourseById(courseId);

            if (student == null || course == null) return;

            var enrollment = new Enrollment
            {
                Student = student,
                Course = course,
                EnrollmentId = _nextId++,
                EnrollmentDate = DateTime.Now
            };

            Enrollments.Add(enrollment);
            student.Enrollments.Add(enrollment);
            course.Enrollments.Add(enrollment);


            
        }

        public void DeleteEnrollment(int enrollmentId)
        {
            var enrollment = Enrollments.FirstOrDefault(e =>  e.EnrollmentId == enrollmentId);

            if (enrollment == null) return;

            enrollment.Student.Enrollments.Remove(enrollment);
            enrollment.Course.Enrollments.Remove(enrollment);
            Enrollments.Remove(enrollment);
        }

        public List<Enrollment> GetAllEnrollments()
        {
            return Enrollments;
        }

        public List<Enrollment> GetEnrollmentsByCourseId(int courseId)
        {
            return Enrollments.Where(e =>  e.CourseId == courseId).ToList();
        }

        public List<Enrollment> GetEnrollmentsByStudentId(int studentId)
        {
            return Enrollments.Where(e => e.StudentId == studentId).ToList();
        }
        
        public void AddEnrollments(Enrollment enrollment)
        {
            var student = _studentService.GetStudentById(enrollment.StudentId);
            var course = _courseService.GetCourseById(enrollment.CourseId);

            if (student == null || course == null) return;

            enrollment.EnrollmentId = _nextId++;
            enrollment.EnrollmentDate = enrollment.EnrollmentDate == default
                ? DateTime.Now
                : enrollment.EnrollmentDate;
            enrollment.Student = student;
            enrollment.Course = course;

            Enrollments.Add(enrollment);
            student.Enrollments.Add(enrollment);
            course.Enrollments.Add(enrollment);
        }

    }
}
