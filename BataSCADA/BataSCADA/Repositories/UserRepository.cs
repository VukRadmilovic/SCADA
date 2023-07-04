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
    }
}
