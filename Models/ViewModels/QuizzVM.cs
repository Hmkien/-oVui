namespace DoVuiHaiNao.Models.ViewModels
{
    public class QuizzVM
    {
        public Quizz Quizz { get; set; } = default!;
        public List<Quizz>? Quizzs { get; set; }
        public int NumberQuestion { get; set; }
    }
}
