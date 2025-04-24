using EasyTime.Application.Contract.Dtos;
using EasyTime.Model.Models;

namespace EasyTime.Application.Contract.IServices
{
   public interface IBaseService<TDto,Tkey,Tentity> where TDto : BaseDto<Tkey> where Tkey : struct where Tentity : BaseEntity<Tkey>
    {
        Task Create(TDto dto);
        Task Update(TDto dto);
        Task Delete(Tkey id);
        Task<List<TDto>> GetAll();
    }
}
