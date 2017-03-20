using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CkeckIn.Models.Questions.Answer
{
    public class AnswerView
    {
        [Display(Name ="Ид ответа")]
        public Guid AnswerId { get; set; }

        public Guid QuestionId { get; set; }


        [Display(Name ="Ответ")]
        public string AnswerMessage { get; set; }

        [Display(Name ="Оценка за ответ")]
        public short Mark { get; set; }
    }
}
