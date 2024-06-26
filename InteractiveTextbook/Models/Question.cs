using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InteractiveTextbook.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string QuizQuestion { get; set; }
        public string AnsA { get; set; }
        public string AnsB { get; set; }
        public string AnsC { get; set; }
        public string AnsD { get; set; }
        public int AnswerId { get; set; }

        [ForeignKey("AnswerId")]
        public Answer CorrectAnswer { get; set; }
    }
}