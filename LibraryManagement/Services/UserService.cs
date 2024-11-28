using LibraryManagement.Interfaces;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Services;

public class UserService
    : IUserService
{
    public Task<JsonResult> AddUser(UserViewModel user)
    {
        throw new NotImplementedException();
    }
}