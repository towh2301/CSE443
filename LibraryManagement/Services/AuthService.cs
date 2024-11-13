using LibraryManagement.HttpContext;
using LibraryManagement.Interfaces;
using Microsoft.AspNet.Identity;


namespace LibraryManagement.Services;

public class AuthService(IHttpContextAccessor httpContextAccessor, IJwtService jwtService, ILogger<AuthService> logger) : IAuthService
{
    public bool IsUserLoggedIn()
    {
        var context = httpContextAccessor.HttpContext;
        return context != null && HttpContextExtensions.IsUserLoggedIn(context);
    }

    public string GetCurrentUserName()
    {
        return httpContextAccessor.HttpContext?.User?.Identity?.GetUserName();
    }

    public string GetUserRole()
    {
        var context = httpContextAccessor.HttpContext;
        if (context?.User?.Identity?.IsAuthenticated != true) return null;

        return context.User.IsInRole("admin") ? "admin" : "user";
    }

    public async Task<bool> ValidateToken()
    {
        try
        {
            var context = httpContextAccessor.HttpContext;
            var token = context?.Request.Cookies["JWTToken"] ?? 
                        context?.Session.GetString("JWTToken");
            
            return !string.IsNullOrEmpty(token);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error validating token");
            return false;
        }
    }
}
