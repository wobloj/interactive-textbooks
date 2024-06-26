using InteractiveTextbook.Enums;

namespace InteractiveTextbook.Models.ViewModels.BookViewModel
{
    public class EditBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public InteractiveElementType InteractiveElementType { get; set; }
        public string? URL { get; set; }
        public string? QuizQuestion { get; set; }
        public string? AnsA { get; set; }
        public string? AnsB { get; set; }
        public string? AnsC { get; set; }
        public string? AnsD { get; set; }
        public string? CorrectAnswer { get; set; }
    }
}
