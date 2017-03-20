using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Thread;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CkeckIn.Models.Questions.Test.Abstract
{
    public class TestCreateModel
    {
        public SelectList FactoryMethodSelectList => new SelectList(Enum.GetNames(typeof(TestFactoryMethod)));
        public SelectList ThreadSelectList() => new SelectList(ThreadViews, "ThreadId", "ThreadName", View.ThreadId);
        public List<ThreadView> ThreadViews { get; set; }
        public TestCreateView View { get; set; }
    }

    public class TestCreateView
    {
        [Display(Name = "Тема")]
        public Guid ThreadId { get; set; }

        [Display(Name = "Название")]
        public string TestName { get; set; }

        //================================================
        [Display(Name = "Время на выполнение")]
        public int TimetoPerformInSeconds { get; set; }


        [Display(Name = "Отображение")]
        public TestFactoryMethod FactoryMethod { get; set; }
    }
}
