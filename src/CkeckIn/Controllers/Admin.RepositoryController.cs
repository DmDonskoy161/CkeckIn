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
                    DisciplineName = "���������������� 1-�", ThreadEntities = new List<ThreadEntity>()
                    {
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "��������� ������� 1�:�����������"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "������������ �1 �: ����������� 8.0�"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "�������������� �������� �������� ������������ �1�: �����������"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "����� ������ � ������������ �1 � ����������� 8.0�"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "������� ������������� ������� ��� ������� ���������� �����"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "��������� �������"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "������� ������� 1 � �����������"},
                    }
                }, new DisciplineEntity
                {
                    DisciplineId = Guid.NewGuid(),
                    DisciplineName = "������ ����������������", ThreadEntities = new List<ThreadEntity>
                    {
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "������ ��������������"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "����� ����������������"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "���� ������"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "��������� � �������"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "��������-��������������� ����������������"},
                    }
                }, new DisciplineEntity
                {
                    DisciplineId = Guid.NewGuid(),
                    DisciplineName = "���������� web-�����������", ThreadEntities = new List<ThreadEntity>
                    {
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "HTML"},
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "JavaScript"},
                    }
                },new DisciplineEntity
                {
                    DisciplineId = Guid.NewGuid(),
                    DisciplineName = "������������ ����", ThreadEntities = new List<ThreadEntity>
                    {
                        new ThreadEntity {ThreadId = Guid.NewGuid(), ThreadName = "���������"},
                    }
                }, new DisciplineEntity
                {
                    DisciplineId = Guid.NewGuid(),
                    DisciplineName = "������ ���������� ��������"
                }, new DisciplineEntity
                {
                    DisciplineId = Guid.NewGuid(),
                    DisciplineName = "������ ���������������� �� ������ �������� ������"
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