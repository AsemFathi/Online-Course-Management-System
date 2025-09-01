using Online_Course_Management_System.Models;
using System.Xml.Linq;

namespace Online_Course_Management_System.Services
{
    public class StudentService : IStudentService
    {
        public List<Student> Students = new List<Student>();
        private int _nextId = 1;

        public StudentService()
        {
            AddStudent(new Student
            {
                Name = "Asem Fathi",
                Email = "asem.fathi@example.com"
            });

            AddStudent(new Student
            {
                Name = "Sara Ahmed",
                Email = "sara.ahmed@example.com"
            });

            AddStudent(new Student
            {
                Name = "Mohamed Ali",
                Email = "mohamed.ali@example.com"
            });

            AddStudent(new Student
            {
                Name = "Laila Hassan",
                Email = "laila.hassan@example.com"
            });

            AddStudent(new Student
            {
                Name = "Omar Mostafa",
                Email = "omar.mostafa@example.com"
            });
        }
        public void AddStudent(Student student)
        {
            student.Id = _nextId++;
            Students.Add(student);
        }

        public void EditStudent(Student student)
        {
            var existing = Students.FirstOrDefault(x => x.Id == student.Id);
            if (existing != null)
            {
                existing.Email = student.Email;
                existing.Name = student.Name;
            }
        }

        public List<Student> GetAllStudents()
        {
            return Students;
        }

        public Student GetStudentByName(string Name)
        {
            var student = Students.FirstOrDefault(x => x.Name == Name);
            return student;
        }

        public Student GetStudentById(int id)
        {
            var student = Students.FirstOrDefault(x => x.Id == id);
            return student;
        }

        public void DeleteStudent(int id)
        {
            var student = Students.FirstOrDefault(x => x.Id == id);
            Students.Remove(student);
        }
    }
}
