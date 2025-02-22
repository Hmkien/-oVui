using System.ComponentModel.DataAnnotations;

namespace DoVuiHaiNao.Models.ViewModels
{
    public class QuizSubmissionViewModel
    {
        [Required]
        public int QuizzId { get; set; }

        [Required]
        public List<QuestionViewModel> Questions { get; set; } = new();
        public int DurationInSeconds { get; set; }
    }

    public class QuestionViewModel
    {
        public int? Id { get; set; }
        public int QuizzId { get; set; }

        [Required(ErrorMessage = "Câu hỏi không được để trống")]
        public string? QuestionText { get; set; }

        public IFormFile? File { get; set; }
        public string? Image { get; set; }
        [Required]
        public List<AnswerViewModel> Answers { get; set; } = new();
    }

    public class AnswerViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string? AnswerText { get; set; }
        public bool IsCorrect { get; set; }

        public IFormFile? File { get; set; }
        public string? Image { get; set; }
    }

}