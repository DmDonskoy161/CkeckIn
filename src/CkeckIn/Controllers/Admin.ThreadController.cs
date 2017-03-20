using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Data;
using CkeckIn.Models;
using CkeckIn.Models.Questions.Thread;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CkeckIn.Controllers
{
    public class ThreadController : Controller
    {
        //=======================================================================================
        public IActionResult Index(Guid? id)
        {
            using (var client = new ThreadRepository())
            {
                return View(new ThreadViewModel
                {
                    ThreadViews = client.TakeAllView(),
                    DisciplineItems = new SelectList(client.Client.DisciplineEntities.ToList(), "DisciplineId", "DisciplineName", id)
                });
            }
        }

        public IActionResult Detail(Guid? id)
        {
            using (var client = new ThreadRepository())
            {
                ThreadEntity entity = client.FindById(id);

                if (entity == null)
                {
                    return View("NotFoundPageView", new NotFoundPageView
                    {
                        Action = "ViewThread",
                        Controller = "Question",
                        Title = "Выберите одну из тем",
                        Message = "Не удалось найти выбранную тему",
                        EntitySelectList = new SelectList(client.TakeAll(), "ThreadId", "ThreadName"),
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
        public IActionResult Create(Guid id)
        {
            using (var client = new ApplicationDbContext())
            {
                return View(new ThreadCreateView
                {
                    DisciplineId = id,
                    DisciplineSelectList = new SelectList(client.DisciplineEntities.ToList(), "DisciplineId", "DisciplineName", id)
                });
            }
        }

        /// <summary>
        /// Добавить вопрос
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ThreadCreateView view)
        {
            using (var client = new ApplicationDbContext())
            {
                if (client.DisciplineEntities.Any(e => e.DisciplineId == view.DisciplineId))
                {
                    client.ThreadEntities.Add(ThreadEntity.Factory(view));
                    client.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    view.DisciplineSelectList = new SelectList(client.DisciplineEntities.ToList(), "DisciplineId", "DisciplineName");
                    ModelState.AddModelError(nameof(view.DisciplineId), "Discipline not found");
                }
                return View(view);
            }
        }
    }
}
