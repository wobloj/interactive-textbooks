using InteractiveTextbook.Data;
using InteractiveTextbook.Enums;
using InteractiveTextbook.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InteractiveTextbook.Services
{
    public class UserService : IUserService
    {
        private readonly InteractiveTextbookDbContext _context;

        public UserService(InteractiveTextbookDbContext context)
        {
            _context = context;
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public User Authenticate(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
            return user;
        }

        public List<User> GetUsers()
        {
            return _context.Users
                .Include(u => u.Student)
                .Include(u => u.Teacher)
                .ToList();
        }

        public void AddUser(string firstName, string lastName, string email, string password, UserType type, string studentClass, string schoolSubject)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                Type = type
            };

            if (type == UserType.Teacher)
            {
                var teacher = new Teacher
                {
                    User = user,
                    SchoolSubject = schoolSubject
                };
                _context.Teachers.Add(teacher);
            }
            else if (type == UserType.Student)
            {
                var student = new Student
                {
                    User = user,
                    Class = studentClass
                };
                _context.Students.Add(student);
            }

            _context.SaveChanges();
        }

        public void DeleteUser(int Id, UserType type)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == Id);
            if (user != null)
            {
                if (type == UserType.Teacher)
                {
                    var teacher = _context.Teachers.FirstOrDefault(x => x.UserId == Id);
                    if (teacher != null)
                    {
                        _context.Teachers.Remove(teacher);
                    }
                }
                else if (type == UserType.Student)
                {
                    var student = _context.Students.FirstOrDefault(x => x.UserId == Id);
                    if (student != null)
                    {
                        _context.Students.Remove(student);
                    }
                }

                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public void EditUser(int Id, string FirstName, string LastName, string Email, string Password, UserType Type, string SchoolSubject, string Class)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == Id);

            if (user != null)
            {
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.Email = Email;
                user.Password = Password;
                user.Type = Type;

                if (Type == UserType.Teacher)
                {
                    var teacher = _context.Teachers.FirstOrDefault(t => t.UserId == Id);
                    if (teacher != null)
                    {
                        teacher.SchoolSubject = SchoolSubject;
                    }
                }
                else if (Type == UserType.Student)
                {
                    var student = _context.Students.FirstOrDefault(s => s.UserId == Id);
                    if (student != null)
                    {
                        student.Class = Class;
                    }
                }

                _context.SaveChanges();
            }
        }

        public Task<List<SelectListItem>> populateUserTypeList()
        {
            return Task.Factory.StartNew(() =>
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){Value = "Student", Text="Uczeń"},
                    new SelectListItem(){Value = "Teacher", Text="Nauczyciel"},
                };
            });
        }
    }
}
