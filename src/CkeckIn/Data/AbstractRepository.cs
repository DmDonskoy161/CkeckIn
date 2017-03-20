using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CkeckIn.Data
{
    public abstract class AbstractRepository<TEntity, TView, TDetailView> : IDisposable
    {
        public readonly ApplicationDbContext Client;
        protected readonly bool AllowDispose;

        protected AbstractRepository()
        {
            Client = new ApplicationDbContext();
            AllowDispose = true;
        }

        protected AbstractRepository(ApplicationDbContext client)
        {
            Client = client;
            AllowDispose = false;
        }

        public abstract TEntity FindById(Guid? id);
        public abstract TDetailView FindByIdView(Guid? id);
        public abstract List<TEntity> TakeAll(Guid? principal);
        public abstract List<TEntity> TakeAll();
        public abstract List<TView> TakeAllView(Guid? principal);
        public abstract List<TView> TakeAllView();

        public void Dispose()
        {
            if (AllowDispose)
            {
                using (Client)
                {
                    Client.SaveChanges();
                }
            }
            else
            {
                Client.SaveChanges();
            }
        }
    }
}
