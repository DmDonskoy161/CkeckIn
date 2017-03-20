using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CkeckIn.Data;
using CkeckIn.Models;
using CkeckIn.Models.Questions.Question;
using CkeckIn.Models.Questions.Test.Abstract;
using CkeckIn.Models.Questions.Test.Question;
using CkeckIn.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CkeckIn.Controllers
{
    public class TestController : Controller
    {
        public static readonly object AddAnswerSyncObject = new object();

        // GET: Test
        public ActionResult Index(Guid? id)
        {
            using (var client = new TestRepository())
            {
                return View(new TestViewModel
                {
                    TestViews = client.TakeAllView(id),
                    ThreadListItems = client.TakeDisciplineThreadGroud(id)
                });
            }
        }

        // GET: Test/Details/5
        public ActionResult Details(Guid? id)
        {
            using (var client = new TestRepository())
            {
                var test = client.FindById(id);

                if (test == null)
                {
                    return View("NotFoundPageView", new NotFoundPageView
                    {
                        Action = "Details",
                        Controller = "Test",
                        Title = "Выберите один из тестов",
                        Message = "Не удалось найти выбранный тест",
                        EntitySelectList = new SelectList(client.TakeAll(), "TestId", "TestName"),
                        ReturnUrl = "Test/Index"
                    });
                }
                return View(test.AsDetailView());
            }
        }

        // GET: Test/Create
        [HttpGet]
        public ActionResult AddQuestion(Guid? id)
        {
            using (var client = new TestRepository())
            {
                TestEntity entity = client.FindById(id);
                
                if (entity == null)
                {
                    return View("NotFoundPageView", new NotFoundPageView
                    {
                        Title = "Выбранный тест не найден",
                        Message = "Выберите один из тестов в списке",
                        EntitySelectList = new SelectList(client.TakeAll(), "TestId", "TestName"),
                        Action = "AddQuestion",
                        Controller = "Test",
                        ReturnUrl = "Test/Index"
                    });
                }

                return View(new TestQuestionCreateView
                {
                    TestId = entity.TestId,
                    TestSelectList = new SelectList(new List<TestEntity> {entity}, "TestId", "TestName", entity.TestId),
                    QuestionSelectList = client.TakeNotContainedQuestionsAsList(entity.TestId),
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestion([FromForm]TestQuestionCreateView view)
        {
            using (var client = new TestRepository())
            {
                var test = client.FindById(view.TestId);
                var quest = client.Client.QuestionEntities.SingleOrDefault(e => e.QuestionId == view.QuestionId);

                if (test == null)
                {
                    ModelState.AddModelError(nameof(view.TestId), "Тест не найден");
                    view.QuestionSelectList = client.TakeNotContainedQuestionsAsList(view.TestId);
                }
                else if (quest == null)
                {
                    ModelState.AddModelError(nameof(view.QuestionId), "Вопрос не найден");
                    view.TestSelectList = new SelectList(new List<TestEntity> { test }, "TestId", "TestName", test.TestId);
                    view.QuestionSelectList = client.TakeNotContainedQuestionsAsList(view.TestId);
                }
                else if (test.ThreadId != quest.ThreadId)
                {
                    ModelState.AddModelError(nameof(view.QuestionId), "Вопрос не пренадлежит теме теста");
                    view.TestSelectList = new SelectList(new List<TestEntity> { test }, "TestId", "TestName", test.TestId);
                    view.QuestionSelectList = client.TakeNotContainedQuestionsAsList(view.TestId);
                }
                else if (test.QuestionEntities.Any(q => q.QuestionId == quest.QuestionId))
                {
                    ModelState.AddModelError(nameof(view.QuestionId), "Вопрос уже присутствует в списке");
                    view.TestSelectList = new SelectList(new List<TestEntity> { test }, "TestId", "TestName", test.TestId);
                    view.QuestionSelectList = client.TakeNotContainedQuestionsAsList(view.TestId);
                }
                else
                {
                    var entity = TestQuestionEntity.Factory(view);
                    client.Client.TestQuestionEntities.Add(entity);
                    client.Client.SaveChanges();
                    return RedirectToAction("Details", new {id = view.TestId});
                }
            }
            return View(view);
            
        }

        // GET: Test/Create
        public ActionResult Create(Guid? id)
        {
            using (var client = new ApplicationDbContext())
            {
                return View(new TestCreateModel
                {
                    ThreadViews = client.ThreadEntities.ToList().Select(t => t.AsView()).ToList(),
                    View = new TestCreateView()
                    {
                        ThreadId = id ?? client.ThreadEntities.FirstOrDefault()?.ThreadId ?? Guid.Empty
                    }
                });
            }
        }

        // POST: Test/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TestCreateModel model)
        {
            using (var client = new ApplicationDbContext())
            {
                if (client.ThreadEntities.Any(t => t.ThreadId == model.View.ThreadId) == false)
                {
                    ModelState.AddModelError("View.ThreadId", "thread not found");
                    return View();
                }

                var test = TestEntity.Factory(model.View, User.GetGuidUserId());
                client.TestEntities.Add(test);
                client.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Test/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}