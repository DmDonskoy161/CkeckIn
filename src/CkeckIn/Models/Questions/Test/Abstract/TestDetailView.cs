using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Test.Question;

namespace CkeckIn.Models.Questions.Test.Abstract
{
    public class TestDetailView : TestView
    {
        public List<TestQuestionView> QuestionViews { get; set; }
    }
}
