namespace DoVuiHaiNao.Models.ViewModels
{
    public class QuizzVM
    {
        public Quizz? Quizz { get; set; }
        public List<Quizz>? Quizzs { get; set; }
        public int NumberQuestion { get; set; }
        public List<Rank>? Rank { get; set; }

    }
    public class Rank
    {
        public string? Name { get; set; }
        public int SoCau { get; set; }
        public double Score { get; set; }
    }
}
