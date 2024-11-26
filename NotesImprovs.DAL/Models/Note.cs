using NotesImprovs.DAL.Interfaces;
using NotesImprovs.Models.ViewModels;

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

    public Note()
    {
        
    }
    
    public Note(string title, string content, Guid userId)
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Title = title;
        Content = content;
        UserId = userId;
    }

    public BaseNoteViewModel ToBaseNoteViewModel()
    {
        return new BaseNoteViewModel() { Title = Title, Content = Content };
    }
    
    public NoteViewModel ToNoteViewModel()
    {
        return new NoteViewModel() { Title = Title, Content = Content, Id = Id};
    }

    public void UpdateNoteData(BaseNoteViewModel noteViewModel)
    {
        Title = noteViewModel.Title;
        Content = noteViewModel.Content;
    }
}