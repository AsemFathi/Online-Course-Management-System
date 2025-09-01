using Online_Course_Management_System.Models;

namespace Online_Course_Management_System.Services
{
    public interface IStudentService
    {
        public List<Student> GetAllStudents();
        public Student GetStudentById(int id);
        public Student GetStudentByName(string Name);
        public void EditStudent(Student student);
        public void AddStudent(Student student);
        public void DeleteStudent(int id);
    }
}
