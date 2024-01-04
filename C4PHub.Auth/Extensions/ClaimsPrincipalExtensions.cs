using C4PHub.Auth.Entities;
using System.Security.Claims;

namespace EasyAuthSamples.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static IEnumerable<UserClaim> GetClaims(this ClaimsPrincipal principal)
        {
            IEnumerable<UserClaim> claims = null;
            if (principal != null)
            {
                claims = principal.Claims.Select(c => new UserClaim(c));
            }
            return claims;
        }
    }
}
