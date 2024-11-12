using LibraryManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManagement.Filters;

public class JwtAuthorizationFilter(IJwtService jwtService) : IAuthorizationFilter
{

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Cookies["JWTToken"] ?? 
                    context.HttpContext.Session.GetString("JWTToken");

        if (!string.IsNullOrEmpty(token)) return;
        context.Result = new RedirectToActionResult("Login", "Account", null);

        // Validate token here if needed
    }
}

