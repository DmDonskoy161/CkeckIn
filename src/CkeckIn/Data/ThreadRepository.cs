using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Thread;
using Microsoft.EntityFrameworkCore;

namespace CkeckIn.Data
{
    public class ThreadRepository : AbstractRepository<ThreadEntity, ThreadView, ThreadDetailView>
    {
        public ThreadRepository(ApplicationDbContext client) : base(client) { }
        public ThreadRepository() { }

        public override ThreadEntity FindById(Guid? id)
        {
            return Client.ThreadEntities
                    .Include(q => q.DisciplineEntity)
                    .Include(q => q.QuestionEntities)
                    .FirstOrDefault(e => e.ThreadId == id);
        }

        public override ThreadDetailView FindByIdView(Guid? id)
        {
            return FindById(id)?.AsDetailView();
        }

        public override List<ThreadEntity> TakeAll(Guid? principal)
        {
            throw new NotImplementedException();
        }

        public override List<ThreadEntity> TakeAll()
        {
            return Client.ThreadEntities
                .Include(q => q.DisciplineEntity)
                .ToList();
        }

        public override List<ThreadView> TakeAllView(Guid? principal)
        {
            return TakeAll().Select(e => e.AsView()).ToList();
        }

        public override List<ThreadView> TakeAllView()
        {
            return TakeAll().Select(e => e.AsView()).ToList();
        }
    }
}
