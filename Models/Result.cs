using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoVuiHaiNao.Models;
public class Result : EntityBase
{
    [Required]
    [ForeignKey("ApplicationUser")]
    public string? UserId { get; set; }
    [ForeignKey("Quizz")]
    public int QuizId { get; set; }
    public int Score { get; set; }
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    public ApplicationUser? User { get; set; }
    public Quizz? Quiz { get; set; }
}

