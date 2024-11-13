using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LibraryManagement.HttpContext;

public static class HttpContextExtensions
{
    public static bool IsUserLoggedIn(Microsoft.AspNetCore.Http.HttpContext context)
    {
        var token = context.Request.Cookies["JWTToken"] ?? 
                    context.Session.GetString("JWTToken");
        return !string.IsNullOrEmpty(token);
    }
    
    public static string GetUserNameFromToken(Microsoft.AspNetCore.Http.HttpContext context)
    {
        var token = context.Request.Cookies["JWTToken"] ?? context.Session.GetString("JWTToken");

        if (string.IsNullOrEmpty(token)) return "null";
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;
        return userName;
    }
}
