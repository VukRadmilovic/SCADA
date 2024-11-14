using Microsoft.EntityFrameworkCore;
using SCADA.Utils;

namespace SCADA.Repositories
{
    public class AlarmLogsRepository
    {
        public static void Delete(int alarmId)
        {
            using var dbContext = new DatabaseContext();
            var logs = dbContext.AlarmLogs.Where(x => x.AlarmId == alarmId);
            foreach (var value in logs)
            {
                dbContext.Entry(value).State = EntityState.Deleted;
            }

            dbContext.SaveChanges();
        }
    }
}
