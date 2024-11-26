using Microsoft.AspNetCore.Identity;
using NotesImprovs.DAL.Interfaces;

namespace NotesImprovs.DAL.Models.Identity;

public class AppRole : IdentityRole<Guid>, IKeyable<Guid>
{
    
}