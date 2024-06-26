using InteractiveTextbook.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InteractiveTextbook.Models
{
    public class InteractiveElement
    {
        private InteractiveElementType ielemtype;

        [Key]
        public int Id { get; set; }
        public InteractiveElementType Type { get => ielemtype; set => ielemtype = value; }
        public int? MediaId {  get; set; }

        [ForeignKey("MediaId")]
        public Media? Media { get; set; }

        public int? QuizId { get; set; }
        [ForeignKey("MediaId")]
        public Quiz? Quiz { get; set; }
    }

}