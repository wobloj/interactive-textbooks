using InteractiveTextbook.Enums;
using System.ComponentModel.DataAnnotations;

namespace InteractiveTextbook.Models
{
    public class User
    {
        private UserType type;

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType Type { get => type; set => type = value; }

        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
    }
}
