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

        public async Task Add(Tentity tentity)
        {
            context.Set<Tentity>().Add(tentity);
        }

        public async Task Delete(Tkey id)
        {
           var entity = await GetById(id);
            entity.IsDelete = true;
        }

        public async Task<IQueryable<Tentity>> GetAllEntities()
        {
            return context.Set<Tentity>().AsNoTracking();
        }

        public async Task<Tentity> GetById(Tkey id)
        {
            return context.Set<Tentity>().Find(id);
        }

        public async Task Update(Tentity tentity)
        {
            context.Set<Tentity>().Update(tentity);
        }

        public async Task SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
