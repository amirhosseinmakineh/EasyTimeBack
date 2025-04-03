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

        public virtual void Create(TDto dto)
        {
            var entity = mapper.Map<Tentity>(dto);
            baseRepository.Add(entity);
            baseRepository.SaveChanges();
        }

        public virtual void Delete(Tkey id)
        {
            baseRepository.Delete(id);
            baseRepository.SaveChanges();
        }

        public virtual List<TDto> GetAll()
        {
            var entity = baseRepository.GetAllEntities();
           var result =  mapper.Map<List<TDto>>(entity);
            baseRepository.SaveChanges();
            return result;
        }

        public void Update(TDto dto)
        {
            var entity = mapper.Map<Tentity>(dto);
            baseRepository.Update(entity);
            baseRepository.SaveChanges();
        }
    }
}
