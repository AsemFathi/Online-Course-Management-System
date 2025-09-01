namespace Online_Course_Management_System.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Guid UserId {  get; set; }
        public User User { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
