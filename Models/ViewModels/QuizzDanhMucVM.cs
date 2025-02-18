using System.ComponentModel.DataAnnotations;

namespace DoVuiHaiNao.Models.ViewModels
{
    public class QuizzDanhMucVM
    {
        public string? Title { get; set; }
        [Display(Name = "Icon")]
        public string? Icon { get; set; }
        [Display(Name = "Màu icon")]
        public string? ColorIcon { get; set; }
        public List<Quizz>? Quizz { get; set; }
        public int QuestionNumber { get; set; }
    }
}
