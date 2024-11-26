using NotesImprovs.DAL.Interfaces;

namespace NotesImprovs.DAL.Models;

public class Note : IKeyable<Guid>, IObservable
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public string Title { get; set; }
    public string Content { get; set; }
    
    public Guid UserId { get; set; }
    
    public AppUser AppUser { get; set; }
}