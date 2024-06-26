using InteractiveTextbook.Enums;

namespace InteractiveTextbook.Models.ViewModels.UserViewModel
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        public string? SchoolSubject { get; set; }
        public string? Class { get; set; }
    }
}
