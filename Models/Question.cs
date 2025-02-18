using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoVuiHaiNao.Models;
public class Question : EntityBase
{
    [ForeignKey("Quizz")]
    public int QuizzId { get; set; }
    [Required]
    [Display(Name = "Câu hỏi")]
    public string? QuestionText { get; set; }
    public Quizz? Quiz { get; set; }
    public ICollection<Answer>? Answers { get; set; }

    [Display(Name = "Hình ảnh")]
    public string? Image { get; set; }
    [NotMapped]
    [Display(Name = "Hình ảnh")]
    public IFormFile? File { get; set; }
}
