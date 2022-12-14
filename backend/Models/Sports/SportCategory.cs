using System.ComponentModel.DataAnnotations;

namespace backend.Models.Sports
{
    public class SportCategory
    {
        [Key]
        public string key { get; set; }
        public string group { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
