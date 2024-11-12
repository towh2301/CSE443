namespace LibraryManagement.Middlewares;

public class JwtMiddleware(RequestDelegate next)
{
    public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
    {
        var token = context.Request.Cookies["JWTToken"] ?? context.Session.GetString("JWTToken");
        
        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers.Add("Authorization", "Bearer " + token);
        }
        
        await next(context);
    }
}


