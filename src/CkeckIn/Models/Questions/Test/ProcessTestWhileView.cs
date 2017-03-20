using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Answer;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CkeckIn.Models.Questions.Test
{
    public class ProcessAnswerModel
    {
        public SelectList AnswerViews { get; set; }

        [Display(Name = "Ответ")]
        public Guid AnswerId { get; set; }
    }


    public class ProcessTestWhileView
    {
        public ProcessDetailView TestDetailView { get; set; }
        public ProcessAnswerModel AnswerModel { get; set; }
        public string QuestionName { get; set; }
        public long CountOfQuestions { get; set; }
        public long CurrentOfQuestions { get; set; }
    }
}
