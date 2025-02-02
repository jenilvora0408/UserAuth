using System.ComponentModel.DataAnnotations;

namespace UserAuth.Entities.Models;

public class Course
{
    [Key]
    public int Id { get; set; }

    public string CourseTitle { get; set; } = null!;
}
