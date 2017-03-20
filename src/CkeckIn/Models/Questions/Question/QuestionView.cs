using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Data;
using CkeckIn.Models.Questions.Answer;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CkeckIn.Models.Questions.Question
{
    public class QuestionViewModel
    {
        public CrudOperationAction? Action { get; set; }
        public List<QuestionView> QuestionViews { get; set; }
        public List<SelectListItem> ThreadItems { get; set; }
    }

    public class QuestionView
    {
        [Display(Name ="Ид вопроса")]
        public Guid QuestionId { get; set; }

        //===========================================

        [Display(Name ="Ид темы")]
        public Guid ThreadId { get; set; }

        [Display(Name ="Тема")]
        public string ThreadName { get; set; }

        [Display(Name ="Ид дисциплины")]
        public Guid DisciplineId { get; set; }

        [Display(Name ="Дисциплина")]
        public string DisciplineName { get; set; }
        //===========================================

        [Display(Name ="Вопрос")]
        public string QuestionMessage { get; set; }

        [Display(Name ="Создан")]
        public DateTime CreationDate { get; set; }

        [Display(Name ="Ответов")]
        public int NumberOfAnswers { get; set; }

        public string QuestionRowStatus => NumberOfAnswers < 3 ? "danger" : string.Empty;

        [Display(Name ="Сложность")]
        public short Strong { get; set; }
    }
}
