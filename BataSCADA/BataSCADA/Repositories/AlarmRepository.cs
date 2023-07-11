using BataSCADA.Models;
using BataSCADA.Utils;
using Microsoft.EntityFrameworkCore;

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

        public static void Update(Alarm alarm, bool triggered = true)
        {
            using var dbContext = new DatabaseContext();
            dbContext.Alarms.Attach(alarm);
            if (triggered)
                dbContext.Entry(alarm).Property(x => x.Triggered).IsModified = true;
            else
                dbContext.Entry(alarm).Property(x => x.SnoozedUntil).IsModified = true;
            dbContext.SaveChanges();
        }

        public static List<Alarm>? GetByTagName(string tagName)
        {
            using var dbContext = new DatabaseContext();
            return dbContext.Alarms.Where(alarm => alarm.AnalogInputTagName == tagName).ToList();
        }

        public static Alarm? GetById(int id)
        {
            using var dbContext = new DatabaseContext();
            return dbContext.Alarms.Any(alarm => alarm.Id == id) ? dbContext.Alarms.FirstOrDefault(alarm => alarm.Id == id) : null;
        }

        public static void Delete(int id)
        {
            var alarm = GetById(id);
            using var dbContext = new DatabaseContext();
            dbContext.Entry(alarm).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }

        public static List<Alarm>? GetActive()
        {
            using var dbContext = new DatabaseContext();
            return dbContext.Alarms
                .Where(a => a.Triggered && (a.SnoozedUntil == null || a.SnoozedUntil <= DateTime.Now)).ToList();
        }
    }
}
