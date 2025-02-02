using System.ComponentModel.DataAnnotations;

namespace UserAuth.Entities.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }

    [EmailAddress(ErrorMessage ="Invalid Email Address")]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
