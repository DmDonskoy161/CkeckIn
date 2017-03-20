using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CkeckIn.Models.Questions.Discipline
{
    public class DisciplineCreateView
    {
        [Display(Name ="Название дисциплины")]
        public string DisciplineName { get; set; }

    }
}
