using System.ComponentModel.DataAnnotations;

namespace UserAuth.Entities.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage ="Username is Required")]
    public string? UserName { get; set; }

    [Required(ErrorMessage ="Password is Required")]
    public string? Password { get; set; }
}
