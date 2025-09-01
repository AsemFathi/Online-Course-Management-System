using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Course_Management_System.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        //foreign keys
        [ForeignKey("Student")]
        public int StudentId {  get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }
}
