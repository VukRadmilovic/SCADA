using System.ComponentModel.DataAnnotations.Schema;
using SCADA.DTOs;

namespace SCADA.Models
{
    public class AddressValue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Address { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
        public Enumerations.ValueType ValueType { get; set; }

        public AddressValue() { }

        public AddressValue(AddressValueDTO value)
        {
            Address = value.Address;
            Value = value.Value;
            ValueType = value.ValueType;
            Timestamp = DateTime.Now;
        }
    }
}
