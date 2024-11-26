using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesImprovs.BLL.Services;
using NotesImprovs.DAL.Models;
using NotesImprovs.Models.ViewModels;

namespace NotesImprovs.API.Controllers;

[Route("api/")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly TokenService _tokenService;
    private readonly AuthRedisService _redisService;


    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService, AuthRedisService redisService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _redisService = redisService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new AppUser() { UserName = model.UserName, Email = model.Email, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow};
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("User registered successfully.");
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return Unauthorized("Invalid login attempt.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            return Unauthorized("Invalid login attempt.");
        }

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        await _redisService.StoreRefreshTokenAsync(user.Id.ToString(), refreshToken, TimeSpan.FromDays(7));

        return Ok(new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }
    
    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId.ToString());
        if (user == null)
        {
            return Unauthorized("Invalid user.");
        }

        var storedRefreshToken = await _redisService.GetRefreshTokenAsync(user.Id.ToString());
        if (storedRefreshToken != model.RefreshToken)
        {
            return Unauthorized("Invalid refresh token.");
        }

        // Generate new tokens
        var newAccessToken = _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        // Update Redis with the new refresh token
        await _redisService.StoreRefreshTokenAsync(user.Id.ToString(), newRefreshToken, TimeSpan.FromDays(7));

        return Ok(new
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }
    
}