using System.ComponentModel.DataAnnotations;

namespace InteractiveTextbook.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public string? CorrectAnswer { get; set; }
    }

}