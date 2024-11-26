using System.ComponentModel.DataAnnotations;

namespace NotesImprovs.Models.ViewModels;

public class LoginUserViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}