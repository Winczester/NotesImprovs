using System.ComponentModel.DataAnnotations;

namespace NotesImprovs.Models.ViewModels;

public class RegisterUserViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    public string UserName { get; set; }
}