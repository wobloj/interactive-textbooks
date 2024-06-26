using InteractiveTextbook.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InteractiveTextbook.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key to User table
        public string SchoolSubject { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
