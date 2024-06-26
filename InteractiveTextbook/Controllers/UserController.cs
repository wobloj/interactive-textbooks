using InteractiveTextbook.Enums;
using InteractiveTextbook.Models;
using InteractiveTextbook.Models.ViewModels.UserViewModel;
using InteractiveTextbook.Services;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveTextbook.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var model = new UserViewModel()
            {
                Users = _userService.GetUsers()
            };
            return View(model);
        }

        public IActionResult AddUser()
        {
            var userType = GetUserType();
            if (userType == UserType.Teacher)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddUser(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _userService.AddUser(model.FirstName, model.LastName, model.Email, model.Password, model.Type, model.Class, model.SchoolSubject);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteUser(int id, UserType type)
        {
            var userType = GetUserType();
            if(userType == UserType.Teacher)
            {
                _userService.DeleteUser(id, type);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditUser(int id)
        {
            var userType = GetUserType();
            if(userType == UserType.Teacher)
            {
                var user = _userService.GetUser(id);
                var model = new EditUserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    Type = user.Type,
                    SchoolSubject = user.Type == UserType.Teacher ? user.Teacher?.SchoolSubject : null,
                    Class = user.Type == UserType.Student ? user.Student?.Class : null
                };

                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditThisUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userService.EditUser(model.Id, model.FirstName, model.LastName, model.Email, model.Password, model.Type, model.SchoolSubject, model.Class);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private UserType GetUserType()
        {
            var userTypeString = HttpContext.Session.GetString("UserType");
            if (Enum.TryParse<UserType>(userTypeString, out var userType))
            {
                return userType;
            }
            return UserType.Student;
        }
    }
}
