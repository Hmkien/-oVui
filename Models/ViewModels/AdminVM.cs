namespace DoVuiHaiNao.Models.ViewModels
{
    public class AdminVM
    {
        public int SoNguoiThamGia { get; set; }
        public int SoQuizz { get; set; }
        public int NguoiThamGia { get; set; }
        public List<Quizz>? QuizzList { get; set; }
    }
}