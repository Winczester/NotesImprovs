using NotesImprovs.BLL.Interfaces;
using NotesImprovs.Models.ViewModels;

namespace NotesImprovs.BLL.Managers;

public class NotesManager : INotesManager
{
    public Task<NoteViewModel> CreateNote(Guid userId, NoteViewModel note)
    {
        throw new NotImplementedException();
    }

    public Task<List<NoteViewModel>> GetNotes(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<NoteViewModel> UpdateNote(Guid noteId, BaseNoteViewModel updateData)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteNote(Guid noteId)
    {
        throw new NotImplementedException();
    }
}