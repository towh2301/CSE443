using LibraryManagement.Models;

namespace LibraryManagement.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}
