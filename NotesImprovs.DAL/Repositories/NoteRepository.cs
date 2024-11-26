using Microsoft.EntityFrameworkCore;
using NotesImprovs.DAL.Interfaces;
using NotesImprovs.DAL.Models;

namespace NotesImprovs.DAL.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly NotesImprovsDbContext _context;

    public NoteRepository(NotesImprovsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Note>> GetAllNotesAsync(Guid userId)
    {
        return await _context.Notes.Where(n => n.UserId == userId).ToListAsync();
    }

    public async Task<Note> GetNoteByIdAsync(Guid noteId)
    {
        return await _context.Notes.FindAsync(noteId);
    }

    public async Task CreateNoteAsync(Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateNoteAsync(Note note)
    {
        _context.Notes.Update(note);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteNoteAsync(Guid noteId)
    {
        var note = await _context.Notes.FindAsync(noteId);
        if (note != null)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }
    }
}