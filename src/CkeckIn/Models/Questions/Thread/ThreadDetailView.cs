using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Question;

namespace CkeckIn.Models.Questions.Thread
{
    public class ThreadDetailView : ThreadView
    {
        public List<QuestionView> QuestionViews { get; set; }
    }
}
