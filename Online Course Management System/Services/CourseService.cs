using Online_Course_Management_System.Models;
using Online_Course_Management_System.Services;

public class CourseService : IcourseService
{
    public List<Course> Courses { get; set; } = new List<Course>();
    private int _next = 1;

    public CourseService()
    {
        AddCourse(new Course
        {
            Title = "ASP.NET Core Razor Pages",
            Description = "Learn how to build web apps with Razor Pages.",
            Price = 199,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddMonths(1)
        });

        AddCourse(new Course
        {
            Title = "C# Advanced Concepts",
            Description = "Deep dive into C# features and best practices.",
            Price = 149,
            StartDate = DateTime.Today.AddDays(7),
            EndDate = DateTime.Today.AddMonths(1)
        });
    }

    public List<Course> GetAllCourses() => Courses;

    public Course GetCourseById(int id) => Courses.FirstOrDefault(x => x.Id == id);

    public void AddCourse(Course course)
    {
        course.Id = _next++;
        Courses.Add(course);
    }

    public void DeleteCourses(int id)
    {
        var course = Courses.FirstOrDefault(c => c.Id == id);
        if (course != null) Courses.Remove(course);
    }

    public void EditCourses(Course course)
    {
        var existing = Courses.FirstOrDefault(x => x.Id == course.Id);
        if (existing != null)
        {
            existing.Title = course.Title;
            existing.Description = course.Description;
            existing.Price = course.Price;
            existing.StartDate = course.StartDate;
            existing.EndDate = course.EndDate;
        }
    }

    public Course GetCourseByTitle(string title) => Courses.FirstOrDefault(x => x.Title == title);

    
}
