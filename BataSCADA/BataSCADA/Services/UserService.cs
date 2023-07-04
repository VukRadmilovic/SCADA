using BataSCADA.DTOs;
using BataSCADA.Models;
using BataSCADA.Repositories;
using BataSCADA.Utils;

namespace BataSCADA.Services
{
    public class UserService
    {
        public static void Login(LoginDTO userCredentials)
        {
            using var dbContext = new DatabaseContext();
            if (!dbContext.Users.Any(user => user.Username == userCredentials.Username))
                throw new ArgumentException("Username or password do not match!");
            User user = dbContext.Users.FirstOrDefault(user => user.Username == userCredentials.Username);
            if (user.Password != userCredentials.Password)
                throw new ArgumentException("Username or password do not match!");
            user.IsLoggedIn = true;
            dbContext.SaveChanges();
        }

        public static void Logout(string username)
        {
            using var dbContext = new DatabaseContext();
            if (!dbContext.Users.Any(user => user.Username == username))
                throw new ArgumentException("Username or password do not match!");
            User user = dbContext.Users.FirstOrDefault(user => user.Username == username);
            user.IsLoggedIn = false;
            dbContext.SaveChanges();
        }

        public static void Register(UserDTO userInfo)
        {
            if (UserRepository.GetByUsername(userInfo.Username) != null)
            {
                throw new ArgumentException("Username already in use!");
            }
            var user = new User(userInfo);
            UserRepository.Save(user);
        }
    }
}
