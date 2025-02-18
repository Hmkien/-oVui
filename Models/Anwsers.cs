using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoVuiHaiNao.Models;
public class Answer : EntityBase
{
    [ForeignKey("Question")]
    public int QuestionId { get; set; }
    [Required]
    [Display(Name = "Câu trả lời")]
    public string? AnswerText { get; set; }
    public bool IsCorrect { get; set; }
    [Display(Name = "Hình ảnh")]
    public string? Image { get; set; }
    public Question? Question { get; set; }
    [NotMapped]
    [Display(Name = "Hình ảnh")]
    public IFormFile? File { get; set; }
}
