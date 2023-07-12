using BataSCADA.DTOs;
using BataSCADA.Models;
using BataSCADA.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BataSCADA.Repositories
{
    public class AddressValueRepository
    {
        public static void SaveValue(AddressValue value)
        {
            using var dbContext = new DatabaseContext();
            dbContext.AddressValues.Add(value);
            dbContext.SaveChanges();
        }

        public static AddressValue GetLastValueByAddress(int address)
        {
            using var dbContext = new DatabaseContext();
            var value = dbContext.AddressValues.OrderByDescending(value => value.Timestamp)
                .FirstOrDefault(value => value.Address == address);
            return value;
        }

        public static void Delete(int address)
        {
            using var dbContext = new DatabaseContext();
            var values = dbContext.AddressValues.Where(x => x.Address == address);
            foreach (var value in values)
            {
                dbContext.Entry(value).State = EntityState.Deleted;
            }

            dbContext.SaveChanges();
        }

        internal static List<AddressValueWithTimeDTO> GetAllValuesByAddress(int address)
        {
            using var dbContext = new DatabaseContext();
            List<AddressValue> values = new List<AddressValue>();
            List<AddressValueWithTimeDTO> values2 = new List<AddressValueWithTimeDTO>();
            values.AddRange(dbContext.AddressValues.OrderByDescending(value => value.Timestamp));
            foreach (AddressValue value in values) { 
                if(value.Address == address)
                {
                    AddressValueWithTimeDTO temp = new AddressValueWithTimeDTO();
                    temp.Address = value.Address;
                    temp.ValueType = value.ValueType;
                    temp.Value = value.Value;
                    temp.Timestamp = value.Timestamp;
                    values2.Add(temp);
                }
            }
            return values2;
        }

        internal static List<AddressValueWithTimeDTO> GetAllValues()
        {
            using var dbContext = new DatabaseContext();
            List<AddressValue> values = new List<AddressValue>();
            List<AddressValueWithTimeDTO> values2 = new List<AddressValueWithTimeDTO>();
            values.AddRange(dbContext.AddressValues.OrderByDescending(value => value.Timestamp));
            foreach (AddressValue value in values)
            {
                    AddressValueWithTimeDTO temp = new AddressValueWithTimeDTO();
                    temp.Address = value.Address;
                    temp.ValueType = value.ValueType;
                    temp.Value = value.Value;
                    temp.Timestamp = value.Timestamp;
                    values2.Add(temp);
            }
            return values2;
        }
    }
}
