using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Thread;

namespace CkeckIn.Models.Questions.Discipline
{
    public class DisciplineView
    {
        [Display(Name = "Ид дисциплины")]
        public Guid DisciplineId { get; set; }

        [Display(Name ="Название дисциплины")]
        public string DisciplineName { get; set; }

        [Display(Name = "Темы")]
        public List<ThreadView> ThreadViews { get; set; }
    }
}
