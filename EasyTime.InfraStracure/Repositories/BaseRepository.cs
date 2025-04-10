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
    }
}
