using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Data;
using CkeckIn.Models;
using CkeckIn.Models.Questions.Discipline;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CkeckIn.Controllers
{
    public class DisciplineController : Controller
    {
        //=======================================================================================
        public IActionResult Index()
        {
            using (var client = new DisciplineRepository())
            {
                return View(client.TakeAllView());
            }
        }

        public IActionResult Detail(Guid? id)
        {
            using (var client = new DisciplineRepository())
            {
                DisciplineEntity entity = client.FindById(id);

                if (entity == null)
                {
                    return View("NotFoundPageView", new NotFoundPageView
                    {
                        Action = "Detail",
                        Controller = "Discipline",
                        Title = "Выберите одну из дисциплин",
                        Message = "Не удалось найти выбранную дисциплину",
                        EntitySelectList = new SelectList(client.TakeAll(), "DisciplineId", "DisciplineName"),
                        ReturnUrl = "Question/Index"
                    });
                }
                return View(entity.AsDetailView());
            }
        }

        /// <summary>
        /// Добавить вопрос
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Добавить вопрос
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DisciplineCreateView view)
        {
            using (var client = new ApplicationDbContext())
            {
                client.DisciplineEntities.Add(DisciplineEntity.Factory(view));
                client.SaveChanges();
                return View();
            }
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Import()
        {
            return View();
        }

        public IActionResult Export()
        {
            return View();
        }
    }
}
