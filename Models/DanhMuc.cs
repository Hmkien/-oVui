using System.ComponentModel.DataAnnotations;

namespace DoVuiHaiNao.Models;

public class DanhMuc : EntityBase
{
    [Required]
    [Display(Name = "Thể loại")]
    public string? Title { get; set; }
    [Display(Name = "Icon")]
    public string? Icon { get; set; }
    [Display(Name = "Màu icon")]
    public string? ColorIcon { get; set; }
    public ICollection<Quizz>? Quizzs { get; set; }
}
