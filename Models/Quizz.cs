using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoVuiHaiNao.Models;
public class Quizz : EntityBase
{
    [Required]
    [Display(Name = "Tiêu đề")]
    public string? Title { get; set; }
    [Display(Name = "Mô tả")]
    [Required]
    public string? Description { get; set; }
    [ForeignKey("DanhMuc")]
    public int DanhMucId { get; set; }
    [Display(Name = "Thời gian làm bài(s)")]
    public int DurationInMinutes { get; set; }
    [Display(Name = "Thể loại")]
    public DanhMuc? DanhMuc { get; set; }
    [Display(Name = "Hình ảnh")]
    public string? ImageQuizz { get; set; }
    public ICollection<Question>? Questions { get; set; }
    [ForeignKey("MucDo")]
    public int MucDoId { get; set; }
    public MucDo? MucDo { get; set; }
    [NotMapped]
    [Display(Name = "Hình ảnh")]
    public IFormFile? File { get; set; }
}

