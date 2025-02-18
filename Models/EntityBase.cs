using System.ComponentModel.DataAnnotations;
using DoVuiHaiNao.Models.Enums;

namespace DoVuiHaiNao.Models;

public class EntityBase
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "Ngày tạo")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    [Display(Name = "Trạng thái")]
    public Status Status { get; set; }
}
