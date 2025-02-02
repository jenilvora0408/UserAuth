using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAuth.Entities.Models;

public class Student
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Enrollment {  get; set; }

    public int Status { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int CourseId { get; set; }

    [ForeignKey(nameof(CourseId))]
    public virtual Course Courses { get; set; } = null!;
}
