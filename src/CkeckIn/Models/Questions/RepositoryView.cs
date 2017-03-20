using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Discipline;
using CkeckIn.Models.Questions.Question;
using CkeckIn.Models.Questions.Test.Abstract;
using CkeckIn.Models.Questions.Thread;

namespace CkeckIn.Models.Questions
{
    public class RepositoryView
    {
        public List<DisciplineView> DisciplineViews { get; set; }
        public List<ThreadView> ThreadViews { get; set; }
        public List<TestView> TestViews { get; set; }
        public List<QuestionView> QuestionViews { get; set; }
    }
}
