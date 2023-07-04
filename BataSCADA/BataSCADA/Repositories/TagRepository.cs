using BataSCADA.Models;
using BataSCADA.Utils;

namespace BataSCADA.Repositories
{
    public class TagRepository
    {
        public static void SaveDigitalOutput(DigitalOutput digitalOutput)
        {
            using var dbContext = new DatabaseContext();
            dbContext.DigitalOutputs.Add(digitalOutput);
            dbContext.SaveChanges();
        }

        public static void SaveDigitalInput(DigitalInput digitalInput)
        {
            using var dbContext = new DatabaseContext();
            dbContext.DigitalInputs.Add(digitalInput);
            dbContext.SaveChanges();
        }

        public static void SaveAnalogOutput(AnalogOutput analogOutput)
        {
            using var dbContext = new DatabaseContext();
            dbContext.AnalogOutputs.Add(analogOutput);
            dbContext.SaveChanges();
        }

        public static void SaveAnalogInput(AnalogInput analogInput)
        {
            using var dbContext = new DatabaseContext();
            dbContext.AnalogInputs.Add(analogInput);
            dbContext.SaveChanges();
        }

        public static Tag? GetTagByTagName(string tagName)
        {
            using var dbContext = new DatabaseContext();
            return dbContext.Tags.Any(tag => tag.TagName == tagName) ? dbContext.Tags.FirstOrDefault(tag => tag.TagName == tagName) : null;
        }
    }
}
