using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Test.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CkeckIn.Models.Questions.Test
{
    public class ProcessTestDetailModel
    {
        public List<ProcessDetailView> TestViews { get; set; }
        public List<SelectListItem> ThreadItems { get; set; }
    }

    public class ProcessDetailView
    {
        public Guid TestId { get; set; }
        public string TestName { get; set; }

        //================================================

        public Guid ThreadId { get; set; }

        [Display(Name = "Тема")]
        public string ThreadName { get; set; }

        public Guid DisciplineId { get; set; }

        [Display(Name = "Дисциплина")]
        public string DisciplineName { get; set; }

        //================================================
        public Guid CreatorId { get; set; }
        [Display(Name = "Преподаватель")]
        public string CreatorName { get; set; }
    }

}
