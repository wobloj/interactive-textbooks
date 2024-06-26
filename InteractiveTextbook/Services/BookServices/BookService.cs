using InteractiveTextbook.Data;
using InteractiveTextbook.Enums;
using InteractiveTextbook.Models;
using InteractiveTextbook.Models.ViewModels.BookViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InteractiveTextbook.Services
{
    public class BookService : IBookService
    {
        private readonly InteractiveTextbookDbContext _context;

        public BookService(InteractiveTextbookDbContext context)
        {
            _context = context;
        }

        public void DeleteBook(int Id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == Id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public GetBookViewModel GetBook(int id)
        {
            var book = _context.Books
                .Include(b => b.InteractiveElement)
                    .ThenInclude(ie => ie.Media)
                .Include(b => b.InteractiveElement)
                    .ThenInclude(ie => ie.Quiz)
                        .ThenInclude(q => q.Question)
                            .ThenInclude(q => q.CorrectAnswer)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return null;
            }

            var viewModel = new GetBookViewModel
            {
                Title = book.Title,
                Description = book.Description,
                Text = book.Text,
                Media = book.InteractiveElement?.Media?.URL,
                Question = book.InteractiveElement?.Quiz?.Question?.QuizQuestion,
                AnsA = book.InteractiveElement?.Quiz?.Question?.AnsA,
                AnsB = book.InteractiveElement?.Quiz?.Question?.AnsB,
                AnsC = book.InteractiveElement?.Quiz?.Question?.AnsC,
                AnsD = book.InteractiveElement?.Quiz?.Question?.AnsD,
                
                InteractiveElementType = book.InteractiveElement.Type
            };

            return viewModel;
        }

        public List<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public void AddBook(string title, string description, string text, InteractiveElementType interactiveElementType, string mediaUrl = null, string quizQuestion = null, string ansA = null, string ansB = null, string ansC = null, string ansD = null, string correctAnswer = null)
        {
            var interactiveElement = new InteractiveElement
            {
                Type = interactiveElementType
            };

            if (interactiveElementType == InteractiveElementType.Media)
            {
                var media = new Media
                {
                    URL = mediaUrl
                };
                interactiveElement.Media = media;
            }
            else if (interactiveElementType == InteractiveElementType.Quiz)
            {
                var answer = new Answer
                {
                    CorrectAnswer = correctAnswer
                };

                var question = new Question
                {
                    QuizQuestion = quizQuestion,
                    AnsA = ansA,
                    AnsB = ansB,
                    AnsC = ansC,
                    AnsD = ansD,
                    CorrectAnswer = answer
                };

                _context.Answers.Add(answer);
                _context.Questions.Add(question);
                _context.SaveChanges();

                var quiz = new Quiz
                {
                    Question = question
                };
                interactiveElement.Quiz = quiz;
            }

            var book = new Book
            {
                Title = title,
                Description = description,
                Text = text,
                InteractiveElement = interactiveElement
            };

            _context.InteractiveElements.Add(interactiveElement);
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(int id, string title, string description, string text, InteractiveElementType interactiveElementType, string mediaUrl = null, string quizQuestion = null, string ansA = null, string ansB = null, string ansC = null, string ansD = null, string correctAnswer = null)
        {
            var book = _context.Books.Include(x => x.InteractiveElement).FirstOrDefault(x => x.Id == id);

            if (book != null)
            {
                book.Title = title;
                book.Description = description;
                book.Text = text;
                book.InteractiveElement.Type = interactiveElementType;

                if(interactiveElementType == InteractiveElementType.Media)
                {
                    var media = _context.Medias.FirstOrDefault(x => x.Id == id);
                    if (media != null)
                    {
                        media.URL = mediaUrl;
                    }
                }

                if (interactiveElementType == InteractiveElementType.Quiz)
                {
                    var quiz = _context.Quizzes
                        .Include(q => q.Question)
                            .ThenInclude(q => q.CorrectAnswer).FirstOrDefault(x => x.Id == id);
                    if (quiz != null)
                    {
                        quiz.Question.QuizQuestion = quizQuestion;
                        quiz.Question.AnsA = ansA;
                        quiz.Question.AnsB = ansB;
                        quiz.Question.AnsC = ansC;
                        quiz.Question.AnsD = ansD;
                        quiz.Question.CorrectAnswer.CorrectAnswer = correctAnswer;
                    }
                }
                _context.SaveChanges();
            }

        }

        public Task<List<SelectListItem>> populateInteractiveElementList()
        {
            return Task.Factory.StartNew(() =>
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){Value = "Media", Text="Załącznik wideo"},
                    new SelectListItem(){Value = "Quiz", Text="Quiz"},
                };
            });
        }
    }
}
