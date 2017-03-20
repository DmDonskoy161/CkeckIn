using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CkeckIn.Models.Questions.Thread
{
    //public class ThreadCreateModel
    //{
    //    public SelectList DisciplineSelectList { get; set; }
    //    public ThreadCreateView View { get; set; }
    //}

    public class ThreadCreateView
    {
        public SelectList DisciplineSelectList { get; set; }

        [Display(Name = "Дисциплина")]
        public Guid DisciplineId { get; set; }

        [Display(Name ="Тема")]
        public string ThreadName { get; set; }
    }
}
