using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Discipline;
using CkeckIn.Models.Questions.Question;
using Microsoft.EntityFrameworkCore;

namespace CkeckIn.Data
{
    public class DisciplineRepository : AbstractRepository<DisciplineEntity, DisciplineView, DisciplineDetailView>
    {
        public DisciplineRepository(ApplicationDbContext client) : base(client) { }
        public DisciplineRepository() { }

        public override DisciplineEntity FindById(Guid? id)
        {
            return Client.DisciplineEntities
                .Include(c => c.ThreadEntities)
                .FirstOrDefault(e => e.DisciplineId == id);
        }

        public override DisciplineDetailView FindByIdView(Guid? id)
        {
            return FindById(id)?.AsDetailView();
        }

        public override List<DisciplineEntity> TakeAll(Guid? principal)
        {
            throw new NotImplementedException();
        }

        public override List<DisciplineEntity> TakeAll()
        {
            return Client.DisciplineEntities
                .Include(e => e.ThreadEntities)
                .ToList();
        }

        public override List<DisciplineView> TakeAllView(Guid? principal)
        {
            throw new NotImplementedException();
        }

        public override List<DisciplineView> TakeAllView()
        {
            return TakeAll().Select(e => e.AsView()).ToList();
        }

    }
}
