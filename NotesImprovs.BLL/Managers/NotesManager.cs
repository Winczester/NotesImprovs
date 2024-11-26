using NotesImprovs.BLL.Interfaces;
using NotesImprovs.BLL.Services;
using NotesImprovs.DAL.Interfaces;
using NotesImprovs.DAL.Models;
using NotesImprovs.Models.ViewModels;

namespace NotesImprovs.BLL.Managers;

public class NotesManager : INotesManager
{
    
    private readonly INoteRepository _noteRepository;
    private readonly NotesRedisService _redisCacheService;

    public NotesManager(INoteRepository noteRepository, NotesRedisService redisCacheService)
    {
        _noteRepository = noteRepository;
        _redisCacheService = redisCacheService;
    }
    
    public async Task<NoteViewModel> CreateNote(Guid userId, BaseNoteViewModel note)
    {
        var noteToCreate = new Note(note.Title, note.Content, userId);
        await _noteRepository.CreateNoteAsync(noteToCreate);
        await _redisCacheService.InvalidateNotesCacheAsync(userId.ToString()); // Invalidate cache after creation
        return noteToCreate.ToNoteViewModel();
    }

    public async Task<List<NoteViewModel>> GetNotes(Guid userId)
    {
        // Check cache first
        var cachedNotes = await _redisCacheService.GetNotesFromCacheAsync(userId.ToString());
        if (cachedNotes != null)
        {
            return cachedNotes.ToList();
        }

        // If not in cache, fetch from database
        var notes = await _noteRepository.GetAllNotesAsync(userId);
        
        // Cache the notes
        await _redisCacheService.SetNotesToCacheAsync(userId.ToString(), notes);

        return notes.Select(n => n.ToNoteViewModel()).ToList();
    }

    public async Task<NoteViewModel> UpdateNote(Guid noteId, BaseNoteViewModel updateData)
    {
        var noteToUpdate = await _noteRepository.GetNoteByIdAsync(noteId);
        if (noteToUpdate == null)
        {
            throw new Exception("Note has not been found!");
        }
        noteToUpdate.UpdateNoteData(updateData);
        await _noteRepository.UpdateNoteAsync(noteToUpdate);
        await _redisCacheService.InvalidateNotesCacheAsync(noteToUpdate.UserId.ToString()); // Invalidate cache after update
        return noteToUpdate.ToNoteViewModel();
    }

    public async Task<bool> DeleteNote(Guid noteId)
    {
        var noteToDelete = await _noteRepository.GetNoteByIdAsync(noteId);
        if (noteToDelete == null)
        {
            throw new Exception("Note has not been found!");
        }
        await _noteRepository.DeleteNoteAsync(noteId);
        await _redisCacheService.InvalidateNotesCacheAsync(noteToDelete.UserId.ToString()); // Invalidate cache after deletion
        return true;
    }
}