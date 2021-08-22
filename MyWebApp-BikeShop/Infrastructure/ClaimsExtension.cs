namespace MyWebApp_BikeShop.Infrastructure
{
    using System.Security.Claims;

    public static class ClaimsExtension
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            try
            {
                return user.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
        }
    }
}
