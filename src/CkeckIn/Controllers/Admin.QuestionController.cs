using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CkeckIn.Data;
using CkeckIn.Models;
using CkeckIn.Models.Questions;
using CkeckIn.Models.Questions.Answer;
using CkeckIn.Models.Questions.Discipline;
using CkeckIn.Models.Questions.Question;
using CkeckIn.Models.Questions.Thread;
using CkeckIn.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CkeckIn.Controllers
{
    /// <summary>
    /// Реализует функции администратора 
    /// для работы со спискои вопросов и ответов
    /// </summary>
    public class QuestionController : Controller
    {
        /// <summary>
        /// Список вопросов
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(Guid? id, CrudOperationAction? operation, long? validity)
        {
            using (var repository = new QuestionRepository())
            {
                return View(new QuestionViewModel
                {
                    QuestionViews = repository.TakeAll(id).Select(e => e.AsView()).ToList(),
                    ThreadItems = TestRepository.TakeDisciplineThreadGroud(repository.Client, id),
                    Action = operation
                });
            }
        }


        //=======================================================================================

        public IActionResult Detail(Guid? id)
        {
            using (var client = new QuestionRepository())
            {
                QuestionEntity entity = client.FindById(id);

                if (entity == null)
                {
                    return View("NotFoundPageView", new NotFoundPageView
                    {
                        Action = "Detail",
                        Controller = "Question",
                        Title = "Выберите один из вопросов",
                        Message = "Не удалось найти выбранный вопрос",
                        EntitySelectList = new SelectList(client.TakeAll(), "QuestionId", "QuestionMessage"),
                        ReturnUrl = "Question/ViewQuestions"
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
        public IActionResult Create(Guid? id)
        {
            using (var client = new ApplicationDbContext())
            {
                return View(new QuestionCreateView()
                {
                    ThreadSelectList = TestRepository.TakeDisciplineThreadGroud(client, id)
                });
            }
        }

        /// <summary>
        /// Добавить вопрос
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(QuestionCreateView view)
        {
            using (var client = new ApplicationDbContext())
            {
                if (client.ThreadEntities.Any(e => e.ThreadId == view.ThreadId))
                {
                    client.QuestionEntities.Add(QuestionEntity.Factory(view, User.GetGuidUserId()));
                    client.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(nameof(view.ThreadId), "ThreadId not found");
                }
                return View();
            }
        }

        //=======================================================================================

        /// <summary>
        /// Добавить вопрос
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ViewAnswer(Guid id)
        {
            using (var client = new ApplicationDbContext())
            {
                var entity = client.AnswerEntities.SingleOrDefault(e => e.AnswerId == id);
                if (entity == null)
                {
                    return BadRequest();
                }

                return View(entity.AsDetailView());
            }
        }

        /// <summary>
        /// Добавить вопрос
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddAnswer(Guid id)
        {
            using (var client = new QuestionRepository())
            {
                return View(new AnswerCreateView
                {
                    QuestionSelectList = new SelectList(client.TakeAll(), "QuestionId", "QuestionMessage")
                });
            }
        }

        /// <summary>
        /// Добавить вопрос
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddAnswer(AnswerCreateView view)
        {
            using (var client = new ApplicationDbContext())
            {
                if (client.QuestionEntities.Any(e => e.QuestionId == view.QuestionId))
                {
                    client.AnswerEntities.Add(AnswerEntity.Factory(view));
                    client.SaveChanges();
                    return RedirectToAction("Detail", new {id = view.QuestionId});
                }
                else
                {
                    ModelState.AddModelError(nameof(view.QuestionId), "QuestionId not found");
                }
                return View();
            }
        }
    }
}
