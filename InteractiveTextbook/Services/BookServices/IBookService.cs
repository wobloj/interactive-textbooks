using InteractiveTextbook.Enums;
using InteractiveTextbook.Models;
using InteractiveTextbook.Models.ViewModels.BookViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InteractiveTextbook.Services
{
    public interface IBookService
    {
        public List<Book> GetBooks();
        public GetBookViewModel GetBook(int Id);
        public void AddBook(string title, string description, string text, InteractiveElementType interactiveElementType, string mediaUrl = null, string quizQuestion = null, string ansA = null, string ansB = null, string ansC = null, string ansD = null, string correctAnswer = null);
        public void DeleteBook(int Id);
        public void UpdateBook(int id, string title, string description, string text, InteractiveElementType interactiveElementType, string mediaUrl = null, string quizQuestion = null, string ansA = null, string ansB = null, string ansC = null, string ansD = null, string correctAnswer = null);
        public Task<List<SelectListItem>> populateInteractiveElementList();
    }
}
