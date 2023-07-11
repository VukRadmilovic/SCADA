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

        public static List<Alarm>? GetByTagName(string tagName)
        {
            using var dbContext = new DatabaseContext();
            return dbContext.Alarms.Where(alarm => alarm.AnalogInputTagName == tagName).ToList();
        }
    }
}
