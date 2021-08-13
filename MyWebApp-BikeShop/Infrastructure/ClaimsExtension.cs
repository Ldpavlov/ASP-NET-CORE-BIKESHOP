namespace MyWebApp_BikeShop.Infrastructure
{
    using System.Security.Claims;

    public static class ClaimsExtension
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
