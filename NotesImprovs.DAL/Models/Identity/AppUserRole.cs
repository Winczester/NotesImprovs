using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using NotesImprovs.DAL.Interfaces;

namespace NotesImprovs.DAL.Models;

public class AppUserRole : IdentityUserRole<Guid>, IKeyable<Guid>
{
    [NotMapped]
    public Guid Id { get; set; }
}