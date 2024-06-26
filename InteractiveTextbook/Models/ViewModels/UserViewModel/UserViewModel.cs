using InteractiveTextbook.Enums;

namespace InteractiveTextbook.Models.ViewModels.UserViewModel
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }

        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}
