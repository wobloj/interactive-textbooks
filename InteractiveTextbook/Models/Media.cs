using System.ComponentModel.DataAnnotations;

namespace InteractiveTextbook.Models
{
    public class Media
    {
        [Key]
        public int Id { get; set; }
        public string? URL { get; set; }
    }
}
