namespace DoVuiHaiNao.Models.ViewModels
{
    public class UserWithRole
    {
        public ApplicationUser? User { get; set; }
        public IList<string>? Role { get; set; }
    }
}
