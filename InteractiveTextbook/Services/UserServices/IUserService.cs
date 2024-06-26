using InteractiveTextbook.Data;
using InteractiveTextbook.Enums;
using InteractiveTextbook.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InteractiveTextbook.Services
{
    public interface IUserService
    {

        public User GetUser(int Id);
        public User Authenticate(string email, string password);
        public List<User> GetUsers();
        public void AddUser(string firstName, string lastName, string email, string password, UserType type, string studentClass, string schoolSubject);
        public void DeleteUser(int Id, UserType type);
        public void EditUser(int Id, string FirstName, string LastName, string Email, string Password, UserType Type, string SchoolSubject, string Class);
        public Task<List<SelectListItem>> populateUserTypeList();
    }
}
