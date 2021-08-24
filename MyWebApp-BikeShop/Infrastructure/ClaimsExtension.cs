namespace MyWebApp_BikeShop.Infrastructure
{
    using System.Security.Claims;
    using static WebConstants;

    public static class ClaimsExtension
    {
        public static string GetId(this ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.NameIdentifier) == null ? null : user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal admin)
            => admin.IsInRole(AdministratorRoleName);
    }
}
