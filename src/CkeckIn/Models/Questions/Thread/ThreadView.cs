using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Question;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CkeckIn.Models.Questions.Thread
{
    public class ThreadViewModel
    {
        public List<ThreadView> ThreadViews { get; set; }
        public SelectList DisciplineItems { get; set; }
    }

    public class ThreadView
    {
        [Display(Name ="Дисциплина")]
        public Guid DisciplineId { get; set; }

        [Display(Name = "Дисциплина")]
        public string DisciplinedName { get; set; }

        [Display(Name ="Ид темы")]
        public Guid ThreadId { get; set; }

        [Display(Name ="Тема")]
        public string ThreadName { get; set; }

        [Display(Name = "Количество вопросов")]
        public long Questions { get; set; }
    }
}
