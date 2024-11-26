using NotesImprovs.Models.ViewModels;

namespace NotesImprovs.BLL.Interfaces;

public interface INotesManager
{ 
    Task<NoteViewModel> CreateNote(Guid userId, NoteViewModel note);
    Task<List<NoteViewModel>> GetNotes(Guid userId);
    Task<NoteViewModel> UpdateNote(Guid noteId, BaseNoteViewModel updateData);
    Task<bool> DeleteNote(Guid noteId);

}