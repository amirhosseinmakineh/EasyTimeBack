using EasyTime.Application.Contract.Dtos;
using EasyTime.Model.Models;

namespace EasyTime.Application.Contract.IServices
{
   public interface IBaseService<TDto,Tkey,Tentity> where TDto : BaseDto<Tkey> where Tkey : struct where Tentity : BaseEntity<Tkey>
    {
        void Create(TDto dto);
        void Update(TDto dto);
        void Delete(Tkey id);
        List<TDto> GetAll();
    }
}
