using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Interfaces;

public interface IUserService
{
    // Add user
    Task<JsonResult> AddUser(UserViewModel user);
}