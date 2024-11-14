using SCADA.DTOs;
using SCADA.Models;
using SCADA.Models.Enumerations;
using SCADA.Repositories;
using SCADA.Utils;

namespace SCADA.Services
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

        public static void Snooze(int id)
        {
            var alarm = AlarmRepository.GetById(id) ?? throw new ArgumentException("Invalid ID");
            var snoozeLength = 30 / alarm.Priority;
            alarm.SnoozedUntil = DateTime.Now.AddSeconds(snoozeLength);
            AlarmRepository.Update(alarm, false);
        }

        internal static List<AlarmLogsDTO> WPriority(int priority)
        {
            var alarms = AlarmRepository.GetByPriority(priority);
            List<AlarmLogsDTO> logs = new List<AlarmLogsDTO>();
            foreach (Alarm alarm in alarms)
            {
                AlarmLogsDTO alarmLogs = new AlarmLogsDTO();
                alarmLogs.Priority = priority;
                alarmLogs.TagName = alarm.AnalogInputTagName;
                alarmLogs.Limit = alarm.Limit;
                alarmLogs.Id = alarm.Id;
                logs.Add(alarmLogs);
            }
            return logs;
            /*List<AlarmLogsDTO> logs = new List<AlarmLogsDTO>();
            List<AlarmLog> allLogs = AlarmRepository.GetLogs();
            foreach (Alarm alarm in alarms)
            {
                foreach (AlarmLog log in allLogs)
                {
                    if (log.Id == alarm.Id)
                    {
                        AlarmLogsDTO logDTO = new AlarmLogsDTO();
                        logDTO.Id = log.Id;
                        logDTO.Priority = priority;
                        logDTO.Limit = log.Limit;
                        logDTO.Value = log.Value;
                        logDTO.Timestamp = log.Timestamp;
                        logDTO.Type = alarm.Type;
                        logs.Add(logDTO);
                    }
                }
            }
            return logs;*/
        }

        internal static List<AlarmLogsDTO> SomeTime(DateTime from, DateTime to)
        {
            var alarms = AlarmRepository.GetByTime(from, to);
            List<AlarmLogsDTO> logs = new List<AlarmLogsDTO>();
            foreach (AlarmLog alarm in alarms)
            {
                if(!alarm.IsTriggered) continue;
                if(alarm.Timestamp > from && alarm.Timestamp < to)
                {
                    AlarmLogsDTO logDTO = new AlarmLogsDTO();
                    logDTO.Id = alarm.Id;
                    logDTO.Limit = alarm.Limit;
                    logDTO.Value = alarm.Value;
                    logDTO.Timestamp = alarm.Timestamp;
                    logDTO.TagName = alarm.TagName;
                    logs.Add(logDTO);
                }
            }
            return logs;
        }
    }
}
