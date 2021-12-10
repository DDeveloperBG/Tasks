using MUSACA.Data;
using MUSACA.Data.Models;
using SIS.WebServer.DataManager;
using System;
using System.Linq;

namespace MUSACA.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public IdentityUser CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,               
                Password = GetHash(password),
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();

            return new IdentityUser
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }

        public IdentityUser GetUser(string username, string password)
        {
            var user = this.db.Users.SingleOrDefault(x => x.Username == username);
            if (user == null || user?.Password != GetHash(password))
            {
                return null;
            }

            return new IdentityUser
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.db.Users.Any(x => x.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.db.Users.Any(x => x.Username == username);
        }

        private string GetHash(string value)
        {
            return Hasher.HashValue(value);
        }
    }
}
