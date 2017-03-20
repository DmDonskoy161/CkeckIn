using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Question;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CkeckIn.Models.Questions.Test.Question
{

    public class TestQuestionCreateView
    {
        [Display(Name = "Тест")]
        public Guid TestId { get; set; }

        public SelectList TestSelectList { get; set; }
        public SelectList QuestionSelectList { get; set; }

        [Display(Name = "Вопрос")]
        public Guid QuestionId { get; set; }

        [Display(Name = "Позиция")]
        public short Position { get; set; }
    }
}
