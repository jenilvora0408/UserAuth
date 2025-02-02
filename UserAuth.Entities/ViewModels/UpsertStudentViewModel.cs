using System.ComponentModel.DataAnnotations;

namespace UserAuth.Entities.ViewModels;

public class UpsertStudentViewModel
{
    public int? StudentId { get; set; }

    [Required]
    [MinLength(2), MaxLength(25)]
    public string? Name { get; set; }

    [Required]
    [StringLength(8)]
    public string? Enrollment { get; set; }

    [Required]
    public int? Status { get; set; }

    [Required]
    public int? CourseId { get; set; }

    [Required]
    public DateTime? DateOfBirth { get; set; }

    public bool CanEdit { get; set; } = false;

    public string? CourseName { get; set; }
}
