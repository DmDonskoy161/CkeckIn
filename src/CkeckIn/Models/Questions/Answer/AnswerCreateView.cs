using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CkeckIn.Models.Questions.Answer
{
    public class AnswerCreateView
    {
        [Display(Name ="Вопрос")]
        public Guid QuestionId { get; set; }

        public SelectList QuestionSelectList { get; set; }


        [Display(Name ="Текст ответа")]
        public string AnswerMessage { get; set; }

        [Range(0, 10)]
        [Display(Name ="Оценка за ответ")]
        public short Mark { get; set; }
    }
}
