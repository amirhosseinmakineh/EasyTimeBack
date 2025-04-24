using AutoMapper;
using EasyTime.Application.Contract.Dtos;
using EasyTime.Application.Contract.IServices;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;

namespace EasyTime.Application.Services
{
    public class BaseService<TDto, Tkey,Tentity> : IBaseService<TDto, Tkey,Tentity> where TDto : BaseDto<Tkey> where Tkey : struct where Tentity : BaseEntity<Tkey>
    {
        private readonly IBaseRepository<Tkey,Tentity> baseRepository;
        private readonly IMapper mapper;

        protected BaseService(Model.IRepository.IBaseRepository<Tkey, Tentity> baseRepository, IMapper mapper)
        {
            this.baseRepository = baseRepository;
            this.mapper = mapper;
        }

        public virtual async Task Create(TDto dto)
        {
            var entity = mapper.Map<Tentity>(dto);
           await baseRepository.Add(entity);
           await baseRepository.SaveChanges();
        }

        public virtual async Task Delete(Tkey id)
        {
           await baseRepository.Delete(id);
           await baseRepository.SaveChanges();
        }

        public virtual async Task<List<TDto>> GetAll()
        {
            var entities = await baseRepository.GetAllEntities(); // ✅ await
            var result = mapper.Map<List<TDto>>(entities);        // ✅ دسترسی به لیست
            return result;
        }


        public async Task Update(TDto dto)
        {
            var entity = mapper.Map<Tentity>(dto);
           await baseRepository.Update(entity);
           await baseRepository.SaveChanges();
        }
    }
}
