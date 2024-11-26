using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesImprovs.BLL.Interfaces;
using NotesImprovs.DAL;
using NotesImprovs.DAL.Models;
using NotesImprovs.Models.ViewModels;

namespace NotesImprovs.API.Controllers;

[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly NotesImprovsDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly INotesManager _notesManager;

    public NotesController(NotesImprovsDbContext context, UserManager<AppUser> userManager, INotesManager notesManager)
    {
        _context = context;
        _userManager = userManager;
        _notesManager = notesManager;
    }

    [Authorize]
    [HttpGet]
    public async Task<List<NoteViewModel>> GetNotes()
    {
        var userId = _userManager.GetUserId(HttpContext.User);
        return await _notesManager.GetNotes(Guid.Parse(userId));
    }

    [Authorize]
    [HttpPost]
    public async Task<NoteViewModel> CreateNote([FromBody] BaseNoteViewModel note)
    {
        var userId = _userManager.GetUserId(HttpContext.User);
        return await _notesManager.CreateNote(Guid.Parse(userId), note);
    }

    [Authorize]
    [HttpPut("{noteId:guid}")]
    public async Task<NoteViewModel> UpdateNote([FromRoute] Guid noteId, [FromBody]BaseNoteViewModel updateData)
    {
        return await _notesManager.UpdateNote(noteId, updateData);
    }

    [Authorize]
    [HttpDelete("{noteId:guid}")]
    public async Task<bool> DeleteNote([FromRoute] Guid noteId)
    {
        return await _notesManager.DeleteNote(noteId);
    }
}