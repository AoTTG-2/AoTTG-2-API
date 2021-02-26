using System.Linq;
using System.Security.Claims;

namespace AoTTG2.IDS.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value ??
                   string.Empty;
        }
    }
}
