using System.Linq;
using System.Security.Claims;
using AoTTG2.IDS.Security;

namespace AoTTG2.IDS.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value ??
                   string.Empty;
        }

        public static bool IsModerator(this ClaimsPrincipal claimPrincipal)
        {
            return claimPrincipal.Claims.Any(x => x.Type == "role" &&
                                                  (x.Value == Roles.Moderator || x.Value == Roles.Administrator));
        }
    }
}
