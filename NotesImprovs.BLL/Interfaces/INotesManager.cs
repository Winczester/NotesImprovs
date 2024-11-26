using NotesImprovs.Models.ViewModels;

namespace NotesImprovs.BLL.Interfaces;

public interface INotesManager
{ 
    Task<NoteViewModel> CreateNote(Guid userId, BaseNoteViewModel note);
    Task<List<NoteViewModel>> GetNotes(Guid userId);
    Task<NoteViewModel> UpdateNote(Guid noteId, Guid userId, BaseNoteViewModel updateData);
    Task<bool> DeleteNote(Guid noteId, Guid userId);

}