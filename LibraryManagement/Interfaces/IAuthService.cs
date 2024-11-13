namespace LibraryManagement.Interfaces;

public interface IAuthService
{
    bool IsUserLoggedIn();
    string GetCurrentUserName();
    string GetUserRole();
    Task<bool> ValidateToken();
}
