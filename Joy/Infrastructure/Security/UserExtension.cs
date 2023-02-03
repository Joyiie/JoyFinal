using System.Security.Claims;

namespace Joy.Infrastructure.Security
{
    public static class UserExtension
    {
        public static string? Name(this ClaimsPrincipal user)
            => user.Identity?.Name;

        public static string? Role(this ClaimsPrincipal user)
            => (user.FindFirstValue("Role")) ?? null;

        public static Guid? Id(this ClaimsPrincipal user)
            => (Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var id)) ? id : null;

    }
}
