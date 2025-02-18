using System.ComponentModel.DataAnnotations;

namespace DoVuiHaiNao.Models
{
    public class MucDo
    {
        public int Id { get; set; }
        [Display(Name ="Mức độ")]
        public string? Name { get; set; }    
    }

}
