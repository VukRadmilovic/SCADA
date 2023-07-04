using BataSCADA.Models;
using BataSCADA.Utils;
using Microsoft.EntityFrameworkCore;

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

        public static void Delete(string tagName)
        {
            Tag tag = GetTagByTagName(tagName);
            using var dbContext = new DatabaseContext();
            dbContext.Entry(tag).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }

        public static Tag? GetTagByTagName(string tagName)
        {
            using var dbContext = new DatabaseContext();
            return dbContext.Tags.Any(tag => tag.TagName == tagName) ? dbContext.Tags.FirstOrDefault(tag => tag.TagName == tagName) : null;
        }

        public static void TurnOnScan(string tagName)
        {
            Tag tag = GetTagByTagName(tagName);
            using var dbContext = new DatabaseContext();
            if (tag is AnalogInput)
            {
                AnalogInput analogTag = (AnalogInput)tag;
                analogTag.IsOn = true;
                dbContext.AnalogInputs.Attach(analogTag);
                dbContext.Entry(analogTag).Property(x => x.IsOn).IsModified = true;
                dbContext.SaveChanges();
            }

            if (tag is DigitalInput)
            {
                DigitalInput digitalTag = (DigitalInput)tag;
                digitalTag.IsOn = true;
                dbContext.DigitalInputs.Attach(digitalTag);
                dbContext.Entry(digitalTag).Property(x => x.IsOn).IsModified = true;
                dbContext.SaveChanges();
            }
            
        }

        public static void TurnOffScan(string tagName)
        {
            Tag tag = GetTagByTagName(tagName);
            using var dbContext = new DatabaseContext();
            if (tag is AnalogInput)
            {
                AnalogInput analogTag = (AnalogInput)tag;
                analogTag.IsOn = false;
                dbContext.AnalogInputs.Attach(analogTag);
                dbContext.Entry(analogTag).Property(x => x.IsOn).IsModified = true;
                dbContext.SaveChanges();
            }

            if (tag is DigitalInput)
            {
                DigitalInput digitalTag = (DigitalInput)tag;
                digitalTag.IsOn = false;
                dbContext.DigitalInputs.Attach(digitalTag);
                dbContext.Entry(digitalTag).Property(x => x.IsOn).IsModified = true;
                dbContext.SaveChanges();
            }

        }
    }
}
