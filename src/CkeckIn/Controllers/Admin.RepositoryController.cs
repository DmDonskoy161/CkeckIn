using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Data;
using CkeckIn.Models.Questions;
using CkeckIn.Models.Questions.Discipline;
using CkeckIn.Models.Questions.Thread;
using Microsoft.AspNetCore.Mvc;

namespace CkeckIn.Controllers
{
    public class RepositoryController : Controller
    {
        public IActionResult Init()
        {
            using (var client = new ApplicationDbContext())
            {
                client.Database.EnsureCreated();
                client.DisciplineEntities.AddRange(new DisciplineEntity[] { new DisciplineEntity
                {
                    DisciplineId = Guid.NewGuid(),
                    DisciplineName = "Программирование 1-С", ThreadEntities = new List<ThreadEntity>()
                    {
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "Концепция системы 1С:Предприятие"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "Конфегурации «1 С: Предприятие 8.0»"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "Характеристика основных объектов конфигурации «1С: Предприятие»"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "Планы счетов в конфигурации «1 С Бухгалтерия 8.0»"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "Примеры использования системы для решения конкретных задач"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "Сервисные функции"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "Объекты системы 1 С Предприятие"},
                    }
                }, new DisciplineEntity
                {
                    DisciplineId = Guid.NewGuid(),
                    DisciplineName = "Основы программирования", ThreadEntities = new List<ThreadEntity>
                    {
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "Основы алгоритмизации"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "Языки программирования"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "Типы данных"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "Процедуры и функции"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "Объектно-ориентированное программирование"},
                    }
                }, new DisciplineEntity
                {
                    DisciplineId = Guid.NewGuid(),
                    DisciplineName = "Разработка web-интерфейсов", ThreadEntities = new List<ThreadEntity>
                    {
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "HTML"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "JavaScript"},
                    }
                },new DisciplineEntity
                {
                    DisciplineId = Guid.NewGuid(),
                    DisciplineName = "Компьютерные сети", ThreadEntities = new List<ThreadEntity>
                    {
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "Топология"},
                    }
                }, new DisciplineEntity
                {
                    DisciplineId = Guid.NewGuid(),
                    DisciplineName = "Пакеты прикладных программ"
                }, new DisciplineEntity
                {
                    DisciplineId = Guid.NewGuid(),
                    DisciplineName = "Основы программирования на языках высокого уровня"
                }});

                client.SaveChanges();
            }
            return Index();
        }

        public IActionResult Index()
        {
            using (var client = new ApplicationDbContext())
            {
                using (var disciplineRepository = new DisciplineRepository(client))
                {
                    using (var threadRepository = new ThreadRepository(client))
                    {
                        using (var questionRepository = new QuestionRepository(client))
                        {
                            return View(new RepositoryView
                            {
                                QuestionViews = questionRepository.TakeAllView(),
                                ThreadViews = threadRepository.TakeAllView(),
                                DisciplineViews = disciplineRepository.TakeAllView(),
                                /*TestViews = client.T.Take(10).ToList().Select(q => new QuestionView()).ToList(),*/
                            });
                        }
                    }
                }
            }
        }
    }
}