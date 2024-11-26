using StackExchange.Redis;

namespace NotesImprovs.BLL.Services;

public class AuthRedisService
{
    private readonly IConnectionMultiplexer _redis;

    public AuthRedisService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task StoreRefreshTokenAsync(string userId, string refreshToken, TimeSpan expiration)
    {
        var db = _redis.GetDatabase();
        await db.StringSetAsync($"refresh_token:{userId}", refreshToken, expiration);
    }

    public async Task<string> GetRefreshTokenAsync(string userId)
    {
        var db = _redis.GetDatabase();
        return await db.StringGetAsync($"refresh_token:{userId}");
    }

    public async Task DeleteRefreshTokenAsync(string userId)
    {
        var db = _redis.GetDatabase();
        await db.KeyDeleteAsync($"refresh_token:{userId}");
    }
}