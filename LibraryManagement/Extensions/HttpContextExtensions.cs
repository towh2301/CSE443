namespace LibraryManagement.HttpContext;

public static class HttpContextExtensions
{
    public static bool IsUserLoggedIn(this Microsoft.AspNetCore.Http.HttpContext context)
    {
        var token = context.Request.Cookies["JWTToken"] ?? 
                    context.Session.GetString("JWTToken");
        return !string.IsNullOrEmpty(token);
    }
}
