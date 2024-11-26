using Microsoft.AspNetCore.Identity;
using NotesImprovs.DAL.Interfaces;

namespace NotesImprovs.DAL.Models;

public class AppUser : IdentityUser<Guid>, IKeyable<Guid>, IObservable
{

    public AppUser()
    {
        Notes = new HashSet<Note>();
    }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<Note> Notes { get; set; }
}