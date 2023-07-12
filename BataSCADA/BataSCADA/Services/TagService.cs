using BataSCADA.DTOs;
using BataSCADA.Models;
using BataSCADA.Models.Enumerations;
using BataSCADA.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BataSCADA.Services
{
    public class TagService
    {
        public static void AddAnalogInput(AnalogInputDTO tagInfo)
        {
            if (TagRepository.GetTagByTagName(tagInfo.TagName) != null)
                throw new ArgumentException("TagName already in use!");
            if(TagRepository.GetTagByAddress(tagInfo.Address) != null)
                throw new ArgumentException("Address already in use!");
            var tag = new AnalogInput(tagInfo);
            TagRepository.SaveAnalogInput(tag);
        }

        public static void AddAnalogOutput(AnalogOutput tagInfo)
        {
            if (TagRepository.GetTagByTagName(tagInfo.TagName) != null)
                throw new ArgumentException("TagName already in use!");
            if (TagRepository.GetTagByAddress(tagInfo.Address) != null)
                throw new ArgumentException("Address already in use!");
            tagInfo.Value = tagInfo.InitialValue;
            TagRepository.SaveAnalogOutput(tagInfo);
        }

        public static void AddDigitalOutput(DigitalOutput tagInfo)
        {
            if (TagRepository.GetTagByTagName(tagInfo.TagName) != null)
                throw new ArgumentException("TagName already in use!");
            if (TagRepository.GetTagByAddress(tagInfo.Address) != null)
                throw new ArgumentException("Address already in use!");
            tagInfo.Value = tagInfo.InitialValue;
            TagRepository.SaveDigitalOutput(tagInfo);
        }

        public static void AddDigitalInput(DigitalInput tagInfo)
        {
            if (TagRepository.GetTagByTagName(tagInfo.TagName) != null)
                throw new ArgumentException("TagName already in use!");
            if (TagRepository.GetTagByAddress(tagInfo.Address) != null)
                throw new ArgumentException("Address already in use!");
            TagRepository.SaveDigitalInput(tagInfo);
        }

        public static void DeleteTag(string tagName)
        {
            if (TagRepository.GetTagByTagName(tagName) == null)
                throw new ArgumentException("Tag with the specified name does not exist!");
            TagRepository.Delete(tagName);
        }

        public static void TurnOnTag(string tagName)
        {
            Tag? tag = TagRepository.GetTagByTagName(tagName);
            if (tag == null)
                throw new ArgumentException("Tag with the specified name does not exist!");
            if (tag is DigitalOutput || tag is AnalogOutput)
                throw new ArgumentException("Tag is not an input tag!");
            TagRepository.TurnOnScan(tagName);
        }

        public static void TurnOffTag(string tagName)
        {
            var tag = TagRepository.GetTagByTagName(tagName);
            if (tag == null)
                throw new ArgumentException("Tag with the specified name does not exist!");
            if (tag is DigitalOutput || tag is AnalogOutput)
                throw new ArgumentException("Tag is not an input tag!");
            TagRepository.TurnOffScan(tagName);
        }

        public static List<OutputTagDTO> GetAllOutputTags()
        {
            var outputTags = TagRepository.GetAllOutputTags();
            List<OutputTagDTO> outputTagDtos = new();
            foreach (var tag in outputTags)
            {
                outputTagDtos.Add(tag is DigitalOutput
                    ? new OutputTagDTO((DigitalOutput)tag)
                    : new OutputTagDTO((AnalogOutput)tag));
            }
            return outputTagDtos;
        }

        public static List<InputTagDTO> GetAllInputTags()
        {
            var inputTags = TagRepository.GetAllInputTags();
            List<InputTagDTO> inputTagDtos = new();
            foreach (var tag in inputTags)
            {
                inputTagDtos.Add(tag is DigitalInput
                    ? new InputTagDTO((DigitalInput)tag)
                    : new InputTagDTO((AnalogInput)tag));
            }
            return inputTagDtos;
        }


        public static void ChangeOutputTagValue(string tagName,double value)
        {
            var tag = TagRepository.GetTagByTagName(tagName);
            if (tag == null)
                throw new ArgumentException("Tag with the specified name does not exist!");
            if (tag is DigitalInput || tag is AnalogInput)
                throw new ArgumentException("Tag is not an output tag!");
            if (tag is AnalogOutput)
            {
                AnalogOutput analogTag = (AnalogOutput)tag;
                analogTag.Value = value;
                TagRepository.ChangeAnalogOutputValue(analogTag);
            }

            if (tag is DigitalOutput)
            {
                DigitalOutput digitalTag = (DigitalOutput)tag;
                digitalTag.Value = value == 0 ? false : true;
                TagRepository.ChangeDigitalOutputValue(digitalTag);
            }
        }

        internal static double ScanInputTag(string tagName)
        {
            var tag = TagRepository.GetTagByTagName(tagName);
            if (tag == null)
                throw new ArgumentException("Tag with the specified name does not exist!");
            if (tag is DigitalOutput || tag is AnalogOutput)
                throw new ArgumentException("Tag is not an input tag!");

            var milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            double value = 0;
            if (tag.Address.Equals(18))
            {
                value = Math.Sin(milliseconds);
            }
            else if (tag.Address.Equals(19))
            {
                value = Math.Cos(milliseconds);
            }
            if (tag.Address.Equals(20) || tag is DigitalInput) // ramp simulation
            {
                if (milliseconds % 2 == 0)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                }
            }

            double minVal = -1;
            double maxVal = -1;

            if (tag is AnalogInput analogTag)
            {
                minVal = analogTag.LowLimit;
                maxVal = analogTag.HighLimit;
            }
            else if (tag is DigitalInput)
            {
                minVal = 0;
                maxVal = 1;
            }

            value = ((value + 1) / 2) * (maxVal - minVal) + minVal;

            if (tag is AnalogInput analogInput)
            {
                AlarmService.TriggerIfNeeded(analogInput, value);
            }

            return value;
        }

        internal static void AddAlarm(Alarm alarm, string tagName)
        {
            var tag = TagRepository.GetTagByTagName(tagName);
            if (tag == null)
                throw new ArgumentException("Tag with the specified name does not exist!");
            if (tag is not AnalogInput input)
                throw new ArgumentException("Tag is not an analog input tag!");

            alarm.AnalogInputTagName = tagName;
            AlarmRepository.Save(alarm);
        }

        internal static double TagLastValue(string tagName)
        {
            var tag = TagRepository.GetTagByTagName(tagName);
            if (tag == null)
                throw new ArgumentException("Tag with the specified name does not exist!");
            if (tag is DigitalOutput || tag is AnalogOutput)
                throw new ArgumentException("Tag is not an input tag!");
            if (tag is DigitalInput)
            {
                if (((DigitalInput)tag).Driver == DriverType.Simulation)
                {
                    return ScanInputTag(tag.TagName);
                }
                return AddressValueRepository.GetLastValueByAddress(tag.Address).Value;
            }
            if (tag is AnalogInput)
            {
                if (((AnalogInput)tag).Driver == DriverType.Simulation)
                {
                    return ScanInputTag(tag.TagName);
                }
                return AddressValueRepository.GetLastValueByAddress(tag.Address).Value;
            }
            return 0.0;
        }

        internal static List<AddressValueWithTimeDTO> TagAllValues(string tagName)
        {
            /*var tag = TagRepository.GetTagByTagName(tagName);
            if (tag == null)
                throw new ArgumentException("Tag with the specified name does not exist!");
            if (tag is DigitalOutput || tag is AnalogOutput)
                throw new ArgumentException("Tag is not an input tag!");*/

            return AddressValueRepository.GetAllValuesByAddress(int.Parse(tagName));
        }

        internal static DigitalWithTime TagLastValueWithTime(string tagName)
        {
            var tag = TagRepository.GetTagByTagName(tagName);
            if (tag == null)
                throw new ArgumentException("Tag with the specified name does not exist!");
            if (tag is DigitalOutput || tag is AnalogOutput)
                throw new ArgumentException("Tag is not an input tag!");
            DigitalWithTime value = new DigitalWithTime();
            
            AddressValue addressValue = AddressValueRepository.GetLastValueByAddress(tag.Address);
            DigitalWithTime ret = new DigitalWithTime();
            ret.Value = addressValue.Value;
            ret.Timestamp = addressValue.Timestamp;
            return ret;
        }

        internal static List<DigitalWithTime> DigitalLast()
        {
            List<InputTagDTO> tags = GetAllInputTags();
            List<DigitalWithTime> ret = new List<DigitalWithTime >();
            foreach (InputTagDTO tag in tags)
            {
                if(tag.Type == TagType.Digital)
                {
                    DigitalWithTime temp = TagLastValueWithTime(tag.TagName);
                    temp.TagName = tag.TagName;
                    temp.Address = tag.Address;
                    ret.Add(temp);
                }
            }
            return ret;
            /*foreach (AddressValueWithTimeDTO value in values)
            {
                foreach(InputTagDTO tag in tags)
                {
                    if(tag.Type != TagType.Digital) continue;
                    if(tag.Address == value.Address)
                    {
                        DigitalWithTime temp = new DigitalWithTime();
                        temp.Address = value.Address;
                        temp.TagName = tag.TagName;
                        temp.Value = value.Value;
                        temp.Timestamp = value.Timestamp;
                        ret.Add(temp);
                    }
                }
            }
            return ret;*/
        }

        internal static object? AnalogLast()
        {
            List<InputTagDTO> tags = GetAllInputTags();
            List<DigitalWithTime> ret = new List<DigitalWithTime>();
            foreach (InputTagDTO tag in tags)
            {
                if (tag.Type == TagType.Analog)
                {
                    DigitalWithTime temp = TagLastValueWithTime(tag.TagName);
                    temp.TagName = tag.TagName;
                    temp.Address = tag.Address;
                    ret.Add(temp);
                }
            }
            return ret;
        }

        internal static List<DigitalWithTime> AllTime(DateTime from, DateTime to)
        {
            List<InputTagDTO> tags = GetAllInputTags();
            List<AddressValueWithTimeDTO> values = AddressValueRepository.GetAllValues();
            List<DigitalWithTime> ret = new List<DigitalWithTime>();
            foreach (AddressValueWithTimeDTO value in values)
            {
                foreach(InputTagDTO tag in tags)
                {
                    if(tag.Address == value.Address)
                    {
                        DigitalWithTime temp = new DigitalWithTime();
                        temp.Address = value.Address;
                        temp.TagName = tag.TagName;
                        temp.Value = value.Value;
                        temp.Timestamp = value.Timestamp;
                        ret.Add(temp);
                    }
                }
            }
            return ret;
        }
    }
}
