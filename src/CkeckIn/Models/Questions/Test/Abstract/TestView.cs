using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CkeckIn.Models.Questions.Test.Abstract
{
    public class TestViewModel
    {
        public List<TestView> TestViews { get; set; }
        public List<SelectListItem> ThreadListItems { get; set; }
    }

    public class TestView
    {
        public Guid TestId { get; set; }
        public string TestName { get; set; }


        //================================================

        public Guid ThreadId { get; set; }

        [Display(Name = "Тема")]
        public string ThreadName { get; set; }

        public Guid DisciplineId { get; set; }

        [Display(Name = "Дисциплина")]
        public string DisciplineName { get; set; }

        //================================================

        [Display(Name = "Создан")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Отображение")]
        public TestFactoryMethod FactoryMethod { get; set; }

        //================================================
        public Guid CreatorId { get; set; }

        [Display(Name = "Преподаватель")]
        public string CreatorName { get; set; }

        //================================================
        public int QuestionCountLimit { get; set; }

        [Display(Name = "Вопросов")]
        public int CountQuestionEntities { get; set; }
    }
}
