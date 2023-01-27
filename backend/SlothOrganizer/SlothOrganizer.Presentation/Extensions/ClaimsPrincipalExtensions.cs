using System.Security.Claims;
using System.Security.Principal;

namespace SlothOrganizer.Presentation.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetId(this ClaimsPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity;
            return long.Parse(identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
