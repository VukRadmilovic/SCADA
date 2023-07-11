using BataSCADA.DTOs;
using BataSCADA.Models;
using BataSCADA.Models.Enumerations;
using BataSCADA.Repositories;
using BataSCADA.Utils;

namespace BataSCADA.Services
{
    public class AlarmService
    {
        public static List<Alarm>? GetByTagName(string tagName)
        {
            return AlarmRepository.GetByTagName(tagName);
        }

        public static void Delete(int id)
        {
            if (AlarmRepository.GetById(id) == null)
                throw new ArgumentException("Alarm with the specified id does not exist!");
            AlarmRepository.Delete(id);
        }

        public static void TriggerIfNeeded(AnalogInput tag, double newValue)
        {
            var alarms = GetByTagName(tag.TagName);
            foreach (var alarm in alarms)
            {
                if (((alarm.Type == AlarmType.High && newValue >= alarm.Limit) ||
                     (alarm.Type == AlarmType.Low && newValue <= alarm.Limit)) &&
                    !alarm.Triggered)
                {
                    alarm.Triggered = true;
                    AlarmRepository.Update(alarm);
                    LogToFile($"Alarm triggered: TagName={tag.TagName}, AlarmId={alarm.Id}, Value={newValue}, Limit={alarm.Limit}");
                    LogToDatabase(tag.TagName, alarm.Id, newValue, alarm.Limit, true);
                }
                else if(!(((alarm.Type == AlarmType.High && newValue >= alarm.Limit) ||
                         (alarm.Type == AlarmType.Low && newValue <= alarm.Limit))) && 
                        alarm.Triggered)
                {
                    alarm.Triggered = false;
                    AlarmRepository.Update(alarm);
                    LogToFile($"Alarm reset: TagName={tag.TagName}, AlarmId={alarm.Id}, Value={newValue}, Limit={alarm.Limit}");
                    LogToDatabase(tag.TagName, alarm.Id, newValue, alarm.Limit, false);
                }
            }
        }

        private static void LogToFile(string message)
        {
            const string logFileName = "logfile.txt";
            var currentDirectory = Directory.GetCurrentDirectory();
            var logFilePath = Path.Combine(currentDirectory, logFileName);
            using var writer = new StreamWriter(logFilePath, true);
            writer.WriteLine($"{DateTime.Now} - {message}");
        }

        private static void LogToDatabase(string tagName, int alarmId, double value, double limit, bool isTriggered)
        {
            using (var dbContext = new DatabaseContext())
            {
                var alarmLog = new AlarmLog
                {
                    TagName = tagName,
                    AlarmId = alarmId,
                    Value = value,
                    Limit = limit,
                    IsTriggered = isTriggered,
                    Timestamp = DateTime.Now
                };

                dbContext.AlarmLogs.Add(alarmLog);
                dbContext.SaveChanges();
            }
        }

        public static List<AlarmDTO>? GetActive()
        {
            var active = AlarmRepository.GetActive();
            var dtos = new List<AlarmDTO>();
            if (active == null) return dtos;
            dtos.AddRange(active.Select(alarm => new AlarmDTO(alarm)));
            return dtos;
        }
    }
}
