using System.Data;
using System.Net;
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

        internal static void Delete(int id)
        {
            var alarm = GetById(id);
            using var dbContext = new DatabaseContext();
            dbContext.Entry(alarm).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }
    }
}
