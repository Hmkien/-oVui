using Microsoft.AspNetCore.Identity;

namespace DoVuiHaiNao.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? DisplayName { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
