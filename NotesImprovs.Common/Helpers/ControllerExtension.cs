using System.Security.Claims;
using Microsoft.VisualBasic;

namespace NotesImprovs.Common.Helpers;

public static class ControllerExtension
{
    public static long GetUserId(this ClaimsPrincipal user)
    {
        return long.Parse(user.Claims.First(u => u.Type == Constants.Strings.JwtClaimIdentifiers.Id).Value);
    }
}