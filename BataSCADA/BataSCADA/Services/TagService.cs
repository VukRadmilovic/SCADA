using BataSCADA.DTOs;
using BataSCADA.Models;
using BataSCADA.Repositories;

namespace BataSCADA.Services
{
    public class TagService
    {
        public static void AddAnalogInput(AnalogInputDTO tagInfo)
        {
            if (TagRepository.GetTagByTagName(tagInfo.TagName) != null)
                throw new ArgumentException("TagName already in use!");
            var tag = new AnalogInput(tagInfo);
            TagRepository.SaveAnalogInput(tag);
        }

        public static void AddAnalogOutput(AnalogOutput tagInfo)
        {
            if (TagRepository.GetTagByTagName(tagInfo.TagName) != null)
                throw new ArgumentException("TagName already in use!");
            TagRepository.SaveAnalogOutput(tagInfo);
        }

        public static void AddDigitalOutput(DigitalOutput tagInfo)
        {
            if (TagRepository.GetTagByTagName(tagInfo.TagName) != null)
                throw new ArgumentException("TagName already in use!");
            TagRepository.SaveDigitalOutput(tagInfo);
        }

        public static void AddDigitalInput(DigitalInput tagInfo)
        {
            if (TagRepository.GetTagByTagName(tagInfo.TagName) != null)
                throw new ArgumentException("TagName already in use!");
            TagRepository.SaveDigitalInput(tagInfo);
        }
    }
}
