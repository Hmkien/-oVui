using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoVuiHaiNao.Models
{
    public class ResultAnswer : EntityBase
    {
        [ForeignKey("Result")]
        public int ResultId { get; set; }
        public virtual Result? Result { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual Question? Question { get; set; }

        [ForeignKey("Answer")]
        public int ChosenAnswerId { get; set; }
        public virtual Answer? Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
