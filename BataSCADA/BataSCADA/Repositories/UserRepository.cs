using BataSCADA.Models;
using BataSCADA.Utils;

namespace BataSCADA.Repositories
{
    public class UserRepository
    {
        public static void Save(User user)
        {
            using var dbContext = new DatabaseContext();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public static User? GetByUsername(string username)
        {
            using var dbContext = new DatabaseContext();
            return dbContext.Users.Any(user => user.Username == username) ? dbContext.Users.FirstOrDefault(user => user.Username == username) : null;
        }

        public static void Login(User user)
        {
            using var dbContext = new DatabaseContext();
            user.IsLoggedIn = true;
            dbContext.Users.Attach(user);
            dbContext.Entry(user).Property(x => x.IsLoggedIn).IsModified = true;
            dbContext.SaveChanges();
        }

        public static void Logout(User user)
        {
            using var dbContext = new DatabaseContext();
            user.IsLoggedIn = false;
            dbContext.Users.Attach(user);
            dbContext.Entry(user).Property(x => x.IsLoggedIn).IsModified = true;
            dbContext.SaveChanges();
        }
    }
}
