using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Answer;


namespace CkeckIn.Models.Questions.Question
{
    public class QuestionDetailView : QuestionView
    {
        public List<AnswerView> AnswerViews { get; set; }
    }
}
