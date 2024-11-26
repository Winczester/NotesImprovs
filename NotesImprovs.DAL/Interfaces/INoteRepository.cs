using NotesImprovs.DAL.Models;

namespace NotesImprovs.DAL.Interfaces;

public interface INoteRepository
{
    Task<List<Note>> GetAllNotesAsync(Guid userId);
    Task<Note> GetNoteByIdAsync(Guid noteId);
    Task CreateNoteAsync(Note note);
    Task UpdateNoteAsync(Note note);
    Task DeleteNoteAsync(Guid noteId);
}