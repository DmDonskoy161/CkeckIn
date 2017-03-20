using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CkeckIn.Models.Questions.Test.Question
{
    public class TestQuestionView
    {
        public Guid TestQuestionId { get; set; }
        public Guid TestId { get; set; }

        public Guid QuestionId { get; set; }
        public string QuestionMessage { get; set; }

        public short Position { get; set; }
    }
}
