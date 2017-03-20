using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Data;
using CkeckIn.Models;
using CkeckIn.Models.Questions.Discipline;
using CkeckIn.Models.Questions.Student.Test;
using CkeckIn.Models.Questions.Test;
using CkeckIn.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CkeckIn.Controllers
{
    /// <summary>
    /// Реализует функции тестирования 
    /// </summary>
    public class ProcessController : Controller
    {
        private async Task<ApplicationUser> GetApplicationUser()
        {
            return await _userManager.FindByIdAsync(User.GetUserId());
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public ProcessController(UserManager<ApplicationUser> userManager, ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }


        [HttpGet]
        public IActionResult Index(Guid? id)
        {
            using (var client = new TestRepository())
            {
                var tests = client.TakeAll(id);
                var view = new ProcessTestDetailModel
                {
                    TestViews = tests.Select(t => t.AsProcessView()).ToList(),
                    ThreadItems = client.TakeDisciplineThreadGroud(id)
                };

                return View(view);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Detail(Guid id)
        {
            using (var client = new ApplicationDbContext())
            {
                using (var repository = new TestRepository(client))
                {
                    var test = repository.FindById(id);
                    if (test == null)
                    {
                        return TravelToSelectTest(repository);
                    }

                    return View(test.AsProcessView());
                }
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> StartProcess(Guid id)
        {
            using (var client = new ApplicationDbContext())
            {
                using (var repository = new TestRepository(client))
                {
                    var test = repository.FindById(id);
                    if (test == null)
                    {
                        return TravelToSelectTest(repository);
                    }
                    //-----------------------------------------------------
                    var user = await GetApplicationUser();
                    user.ActiveTestEntity = new StudentTestEntity
                    {
                        TestId = test.TestId,
                        StartedDateTime = DateTime.UtcNow,
                        StudentId = user.Id,
                        StudentTestId = Guid.NewGuid()
                    };
                    await _userManager.UpdateAsync(user);
                    //-----------------------------------------------------
                    return RedirectToAction("Process");
                }
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Process()
        {
            using (var repository = new TestRepository())
            {
                var user = await GetApplicationUser();
                var userTest = repository.TakeStudentTest(user.ActiveTestId);

                if (user.ActiveTestId == null || userTest?.TestEntity == null)
                {
                    return TravelToSelectTest(repository);
                }

                //-----------------------------------------------------
                if (userTest.TakeQuestion() == null)
                {
                    return RedirectToAction("CompleteProcess");
                }
                return View(userTest.TakeWhileView());
            }
        }

        private IActionResult TravelToSelectTest(TestRepository repository)
        {
            return View("NotFoundPageView", new NotFoundPageView()
            {
                Action = "Detail",
                Controller = "Process",
                Title = "Не удалось найти начатый тест",
                Message = "Выберите один из тестов в списке ниже и попробуйте выполнить его",
                ReturnUrl = "Index",
                EntitySelectList = new SelectList(repository.TakeAll(), "TestId", "TestName")
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CompleteProcess()
        {
            using (var repository = new TestRepository())
            {
                var user = await GetApplicationUser();
                var userTest = repository.TakeStudentTest(user.ActiveTestId);
                if (user.ActiveTestId == null || userTest?.TestEntity == null)
                {
                    return TravelToSelectTest(repository);
                }

                if (userTest.TakeQuestion() != null)
                {
                    return RedirectToAction("Process");
                }

                user.ActiveTestId = null;
                await _userManager.UpdateAsync(user);
                return View(userTest.CompleteTest());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Answer(ProcessAnswerModel model)
        {
            using (var repository = new TestRepository())
            {
                var user = await GetApplicationUser();
                var userTest = repository.TakeStudentTest(user.ActiveTestId);
                userTest.AnswerEntities.Add(new StudentAnswerEntity
                {
                    StudentTestAnswerId = Guid.NewGuid(),
                    AnswedTime = DateTime.UtcNow,
                    AnswerId = model.AnswerId,
                });

                repository.Client.SaveChanges();
                return RedirectToAction("Process");
            }
        }

    }
}
