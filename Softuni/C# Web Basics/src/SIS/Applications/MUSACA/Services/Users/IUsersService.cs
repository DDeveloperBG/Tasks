using MUSACA.ViewModels.Users;
using SIS.WebServer.DataManager;
using System;

namespace MUSACA.Services.Users
{
    public interface IUsersService
    {
        IdentityUser CreateUser(string username, string email, string password);

        bool IsEmailAvailable(string email);

        IdentityUser GetUser(string username, string password);

        bool IsUsernameAvailable(string username);
    }
}
