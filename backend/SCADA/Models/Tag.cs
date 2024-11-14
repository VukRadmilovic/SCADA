using System.ComponentModel.DataAnnotations;

namespace SCADA.Models
{
    public class Tag
    {
        [Key]
        public string TagName { get; set; }
        public string Description { get; set; }
        public int Address { get; set; }

        public Tag()
        {
            TagName = "";
            Description = "";
            Address = -1;
        }

    }
}
