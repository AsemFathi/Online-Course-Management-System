namespace Online_Course_Management_System.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
