using System.Text.Json;
using NotesImprovs.DAL.Models;
using NotesImprovs.Models.ViewModels;
using StackExchange.Redis;

namespace NotesImprovs.BLL.Services;

public class NotesRedisService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly string _cacheKeyPrefix = "user_notes:";

    public NotesRedisService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<IEnumerable<NoteViewModel>> GetNotesFromCacheAsync(string userId)
    {
        var db = _redis.GetDatabase();
        var cachedNotes = await db.StringGetAsync($"{_cacheKeyPrefix}{userId}");
        if (cachedNotes.IsNullOrEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize<IEnumerable<NoteViewModel>>(cachedNotes);
    }

    public async Task SetNotesToCacheAsync(string userId, IEnumerable<Note> notes)
    {
        var db = _redis.GetDatabase();
        var serializedNotes = JsonSerializer.Serialize(notes);
        await db.StringSetAsync($"{_cacheKeyPrefix}{userId}", serializedNotes, TimeSpan.FromMinutes(10)); // Cache for 10 minutes
    }

    public async Task InvalidateNotesCacheAsync(string userId)
    {
        var db = _redis.GetDatabase();
        await db.KeyDeleteAsync($"{_cacheKeyPrefix}{userId}");
    }
}