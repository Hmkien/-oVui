using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DoVuiHaiNao.Models;
public class Result : EntityBase
{
    [Required]
    [ForeignKey("ApplicationUser")]
    public string? UserId { get; set; }
    [ForeignKey("Quizz")]
    [JsonProperty("QuizId")]
    public int QuizId { get; set; }
    [JsonProperty("Score")]
    public double Score { get; set; }
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    public ApplicationUser? User { get; set; }
    public Quizz? Quiz { get; set; }
}

