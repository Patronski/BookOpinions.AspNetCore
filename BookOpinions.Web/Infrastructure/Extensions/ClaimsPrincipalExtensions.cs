using System.Security.Claims;

namespace BookOpinions.Web.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// User ID
        /// </summary>
        public static string GetUserId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return null;

            ClaimsPrincipal currentUser = user;
            return currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
