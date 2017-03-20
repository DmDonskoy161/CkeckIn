using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Question;
using CkeckIn.Models.Questions.Student.Test;
using CkeckIn.Models.Questions.Test;
using CkeckIn.Models.Questions.Test.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CkeckIn.Data
{
    public class TestRepository : AbstractRepository<TestEntity, TestView, TestDetailView>
    {
        public TestRepository() { }
        public TestRepository(ApplicationDbContext client) : base(client) { }

        public List<SelectListItem> TakeDisciplineThreadGroud(Guid? threadId)
        {
            return TakeDisciplineThreadGroud(Client, threadId);
        }

        public static List<SelectListItem> TakeDisciplineThreadGroud(ApplicationDbContext context, Guid? threadId)
        {
            Dictionary<Guid, SelectListGroup> disciplineDictionary = context.DisciplineEntities.Select(d => new
            {
                id = d.DisciplineId,
                item = new SelectListGroup()
                {
                    Name = d.DisciplineName,
                }
            }).ToDictionary(x => x.id, x => x.item);

            return context.ThreadEntities
                .Include(c => c.TestEntities)
                .ToList().Select(t => new SelectListItem
                {
                    Group = disciplineDictionary[t.DisciplineId],
                    Text = $"{t.ThreadName}, {t.TestEntities.Count} тестов",
                    Value = t.ThreadId.ToString(),
                    Selected = t.ThreadId == threadId
                }).ToList();
        }

        public override TestEntity FindById(Guid? id)
        {
            return Client.TestEntities
                    .Include(c => c.CreatorEntity)
                    .Include(c => c.QuestionEntities)
                    .ThenInclude(c => c.QuestionEntity)
                    .Include(c => c.ThreadEntity)
                    .Include(c => c.ThreadEntity.DisciplineEntity)
                    .SingleOrDefault(e => e.TestId == id);
        }

        public override TestDetailView FindByIdView(Guid? id)
        {
            return FindById(id).AsDetailView();
        }

        public override List<TestEntity> TakeAll(Guid? principal)
        {
            var result = TakeAll();
            if (principal.HasValue)
            {
                return result.Where(e => e.ThreadId == principal.Value).ToList();
            }
            return result;
        }

        public List<QuestionEntity> TakeNotContainedQuestions(Guid id)
        {
            var entity = FindById(id);
            return Client.QuestionEntities.Where(q => q.ThreadId == entity.ThreadId).Where(q => entity.QuestionEntities.Select(qt => qt.QuestionId).Contains(q.QuestionId) == false).ToList();
        }

        public SelectList TakeNotContainedQuestionsAsList(Guid id)
        {
            return new SelectList(TakeNotContainedQuestions(id), "QuestionId", "QuestionMessage");
        }

        public override List<TestEntity> TakeAll()
        {
            return Client.TestEntities
                .Include(e => e.ThreadEntity)
                .Include(e => e.ThreadEntity.DisciplineEntity)
                .Include(e => e.CreatorEntity)
                .Include(e => e.QuestionEntities)
                .ToList();
        }

        public override List<TestView> TakeAllView(Guid? principal)
        {
            return TakeAll(principal).Select(e => e.AsView()).ToList();
        }

        public override List<TestView> TakeAllView()
        {
            return TakeAll().Select(e => e.AsView()).ToList();
        }


        public StudentTestEntity TakeStudentTest(Guid? id)
        {
            return Client.StudentTestEntities
                .Include(c => c.AnswerEntities)
                .Include(c => c.TestEntity)
                .ThenInclude(c => c.QuestionEntities)
                .ThenInclude(c => c.QuestionEntity)
                .ThenInclude(c => c.AnswerEntities)
                .Include(c => c.TestEntity)
                .ThenInclude(c => c.ThreadEntity)
                .ThenInclude(c => c.DisciplineEntity)
                .SingleOrDefault(e => e.StudentTestId == id);
        }
    }
}
