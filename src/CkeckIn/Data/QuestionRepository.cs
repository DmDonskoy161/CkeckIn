using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Question;
using Microsoft.EntityFrameworkCore;

namespace CkeckIn.Data
{
    public class QuestionRepository : AbstractRepository<QuestionEntity, QuestionView, QuestionDetailView>
    {
        public QuestionRepository(ApplicationDbContext client) : base(client) { }
        public QuestionRepository() { }

        public override QuestionEntity FindById(Guid? id)
        {
            return Client.QuestionEntities
               .Include(q => q.CreatorEntity)
                .Include(q => q.AnswerEntities)
               .Include(q => q.ThreadEntity)
               .ThenInclude(q => q.DisciplineEntity)
               .FirstOrDefault(e => e.QuestionId == id);
        }

        public override QuestionDetailView FindByIdView(Guid? id)
        {
            return FindById(id)?.AsDetailView();
        }

        public override List<QuestionEntity> TakeAll(Guid? principal)
        {
            var rows = Client.QuestionEntities
                .Include(q => q.CreatorEntity)
                .Include(q => q.ThreadEntity)
                .ThenInclude(q => q.DisciplineEntity)
                .Include(q => q.AnswerEntities);

            return principal != null ? rows.Where(e => e.ThreadId == principal.Value).ToList() : rows.ToList();
        }

        public override List<QuestionEntity> TakeAll()
        {
            return Client.QuestionEntities
                .Include(q => q.CreatorEntity)
                .Include(q => q.ThreadEntity)
                .ThenInclude(q => q.DisciplineEntity)
                .Include(q => q.AnswerEntities)
               .ToList();
        }

        public override List<QuestionView> TakeAllView(Guid? principal)
        {
            throw new NotImplementedException();
        }

        public override List<QuestionView> TakeAllView() => TakeAll().Select(e => e.AsView()).ToList();
    }
}
