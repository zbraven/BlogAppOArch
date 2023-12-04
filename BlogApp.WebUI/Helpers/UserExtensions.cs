using System;
using System.Security.Claims;
using System.Security.Principal;

namespace BlogApp.WebUI.Helpers
{
    public static class UserExtensions
    {
        public static Guid GetUserId(this IPrincipal user)
        {
            var userClaim = user as ClaimsPrincipal;
            return Guid.Parse(userClaim?.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        }
        public static string GetEmployeeFullname(this IPrincipal user)
        {
            var userClaim = user as ClaimsPrincipal;
            return userClaim?.FindFirst(x => x.Type == "FullName")?.Value;
        }

        public static string GetAppUserId(this IPrincipal user)
        {
            var userClaim = user as ClaimsPrincipal;
            return userClaim?.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        }
    }
}