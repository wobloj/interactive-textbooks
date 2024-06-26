using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InteractiveTextbook.Models
{
    public class Book
    {
        private InteractiveElement ielem;

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public int InteractiveElementId { get; set; }

        [ForeignKey("InteractiveElementId")]
        public InteractiveElement InteractiveElement { get => ielem; set => ielem = value; }
    }

}
