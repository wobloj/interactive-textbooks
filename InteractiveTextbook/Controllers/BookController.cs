using InteractiveTextbook.Enums;
using InteractiveTextbook.Models;
using InteractiveTextbook.Models.ViewModels.BookViewModel;
using InteractiveTextbook.Models.ViewModels.UserViewModel;
using InteractiveTextbook.Services;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveTextbook.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _booksService;

        public BookController(ILogger<BookController> logger, IBookService booksService)
        {
            _logger = logger;
            _booksService = booksService;
        }

        public IActionResult Index()
        {
            var model = new BookViewModel()
            {
                Books = _booksService.GetBooks()
            };
            return View(model);
        }

        public IActionResult GetBook(int id)
        {
            var bookViewModel = _booksService.GetBook(id);
            if (bookViewModel == null)
            {
                return NotFound();
            }

            return View(bookViewModel);
        }

        public IActionResult AddBook()
        {
            var userType = GetUserType();
            if (userType == UserType.Student)
            {
                return RedirectToAction("Index");
            }
            else if (userType == UserType.Teacher)
            {
                return View();
            }

            return Forbid(); // Access denied for other user types
        }

        public IActionResult DeleteBook(int id)
        {
            var userType = GetUserType();
            if(userType == UserType.Teacher)
            {
                if (ModelState.IsValid)
                {
                    _booksService.DeleteBook(id);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddBook(AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _booksService.AddBook(
                    model.Title,
                    model.Description,
                    model.Text,
                    model.InteractiveElementType,
                    model.URL,
                    model.QuizQuestion,
                    model.AnsA,
                    model.AnsB,
                    model.AnsC,
                    model.AnsD,
                    model.CorrectAnswer
                );
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult EditBook(int id)
        {
            var userType = GetUserType();
            if (userType == UserType.Teacher)
            {
                var book = _booksService.GetBook(id);
                var model = new EditBookViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    Text = book.Text,
                    InteractiveElementType = book.InteractiveElementType,
                    URL = book.Media,
                    QuizQuestion = book.Question,
                    AnsA = book.AnsA,
                    AnsB = book.AnsB,
                    AnsC = book.AnsC,
                    AnsD = book.AnsD,
                    CorrectAnswer = book.CorrectAnswer,
                };
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditThisBook(EditBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                _booksService.UpdateBook(model.Id, model.Title, model.Description, model.Text, model.InteractiveElementType, model.URL, model.QuizQuestion, model.AnsA, model.AnsB, model.AnsC, model.AnsD, model.CorrectAnswer);
                return RedirectToAction("Index");
            }

            return View("EditBook",model);
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
