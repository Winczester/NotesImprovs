using System.ComponentModel.DataAnnotations;

namespace NotesImprovs.Models.ViewModels;

public class RefreshTokenViewModel
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string RefreshToken { get; set; }
}