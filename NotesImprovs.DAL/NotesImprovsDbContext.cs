using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotesImprovs.DAL.Models;
using NotesImprovs.DAL.Models.Identity;

namespace NotesImprovs.DAL;

public class NotesImprovsDbContext : IdentityDbContext<AppUser, AppRole, Guid, AppUserClaim,  AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
{
    
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<AppUserClaim> UserClaims { get; set; }
    public DbSet<AppUserRole> AppUserRoles { get; set; }
    public DbSet<AppRoleClaim> AppRoleClaims { get; set; }
    public DbSet<AppUserLogin> UserLogins { get; set; }
    public DbSet<AppUserToken> UserTokens { get; set; }
    
    public DbSet<Note> Notes { get; set; }
    
    public NotesImprovsDbContext(DbContextOptions<NotesImprovsDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Note>()
            .HasOne(n => n.AppUser)
            .WithMany(u => u.Notes)
            .HasForeignKey(n => n.UserId);

    }
}