using InteractiveTextbook.Enums;
using Microsoft.AspNetCore.Components.Web;

namespace InteractiveTextbook.Models.ViewModels.BookViewModel
{
    public class GetBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string? Media {  get; set; }
        public string? Question { get; set; }
        public string? AnsA {  get; set; }
        public string? AnsB {  get; set; }
        public string? AnsC {  get; set; }
        public string? AnsD {  get; set; }
        public string? CorrectAnswer {  get; set; }
        public InteractiveElementType InteractiveElementType { get; set; }
    }
}
