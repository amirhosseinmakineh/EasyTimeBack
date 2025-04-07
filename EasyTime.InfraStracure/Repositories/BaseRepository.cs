using EasyTime.InfraStracure.Context;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyTime.InfraStracure.Repositories
{
    public class BaseRepository<Tkey, Tentity> : IBaseRepository<Tkey, Tentity> where Tkey : struct where Tentity : BaseEntity<Tkey>
    {
        private readonly EasyTimeContext context;

        public BaseRepository(EasyTimeContext context)
        {
            this.context = context;
        }

        public void Add(Tentity tentity)
        {
            context.Set<Tentity>().Add(tentity);
        }

        public void Delete(Tkey id)
        {
           var entity =  GetById(id);
            entity.IsDelete = true;
        }

        public IQueryable<Tentity> GetAllEntities()
        {
            return context.Set<Tentity>();
        }

        public Tentity GetById(Tkey id)
        {
            return context.Set<Tentity>().Find(id);
        }

        public void Update(Tentity tentity)
        {
            context.Set<Tentity>().Update(tentity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void DetachIfTracked(Tentity entity)
        {
            var entry = context.ChangeTracker.Entries<Tentity>()
                .FirstOrDefault(e => e.Entity != null && e.Entity.GetType() == typeof(Tentity) && context.Entry(e.Entity).Property("Id").CurrentValue.Equals(context.Entry(entity).Property("Id").CurrentValue));

            if (entry != null)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
