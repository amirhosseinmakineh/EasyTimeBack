using EasyTime.Model.Models;

namespace EasyTime.Model.IRepository
{
   public interface IBaseRepository<Tkey,Tentity> where Tentity : BaseEntity<Tkey> where Tkey : struct
    {
        void Add(Tentity tentity);
        void Update(Tentity tentity);
        void Delete(Tkey id);
        Tentity GetById(Tkey id);
        IQueryable<Tentity> GetAllEntities();
        void SaveChanges();

    }
}
