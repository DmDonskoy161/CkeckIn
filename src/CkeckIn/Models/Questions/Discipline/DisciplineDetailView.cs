using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Question;
using CkeckIn.Models.Questions.Thread;

namespace CkeckIn.Models.Questions.Discipline
{
    public class DisciplineDetailView : DisciplineView
    {
        public List<QuestionView> QuestionViews { get; set; }
    }
}
