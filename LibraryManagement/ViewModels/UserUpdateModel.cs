using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Models;

namespace LibraryManagement.ViewModels;

public class UserUpdateModel
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }

    public User EditUser(User user, UserUpdateModel userUpdateModel)
    {
        user.FullName = userUpdateModel.FullName;
        user.Email = userUpdateModel.Email;
        return user;
    }
}
