using EasyTime.Model.Models;

namespace EasyTime.Model.IRepository
{
    public interface IBaseRepository<Tkey, Tentity> where Tentity : BaseEntity<Tkey> where Tkey : struct
    {
        Task Add(Tentity tentity);
        Task Update(Tentity tentity);
        Task Delete(Tkey id);
        Task<Tentity> GetById(Tkey id);
        Task<IQueryable<Tentity>> GetAllEntities();
        Task SaveChanges();
        Task AddRange(IEnumerable<Tentity> tentities);

    }
}