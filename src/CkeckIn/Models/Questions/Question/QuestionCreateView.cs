using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Thread;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CkeckIn.Models.Questions.Question
{
    public class QuestionCreateView
    {
        public List<SelectListItem> ThreadSelectList { get; set; }

        [Display(Name ="Тема")]
        public Guid ThreadId { get; set; }

        [Display(Name ="Текст вопроса")]
        public string QuestionMessage { get; set; }

        [Display(Name ="Сложность вопроса")]
        public short Strong { get; set; }
    }
}
