using BataSCADA.Models;
using BataSCADA.Utils;

namespace BataSCADA.Repositories
{
    public class AlarmRepository
    {
        public static void Save(Alarm alarm)
        {
            using var dbContext = new DatabaseContext();
            dbContext.Alarms.Add(alarm);
            dbContext.SaveChanges();
        }
    }
}
