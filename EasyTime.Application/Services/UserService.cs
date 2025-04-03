using AutoMapper;
using EasyTime.Application.Contract.Dtos;
using EasyTime.Application.Contract.IServices;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;
using EasyTime.Utilities.Convertor;

namespace EasyTime.Application.Services
{
    public class UserService : BaseService<UserDto, Guid, User>, IUserService
    {
        private readonly IBaseRepository<Guid, User> repository;
        private readonly ITokenGenerator tokenGenerator;
        public UserService(IMapper mapper, IBaseRepository<Guid, User> repository, ITokenGenerator tokenGenerator) : base(repository, mapper)
        {
            this.repository = repository;
            this.tokenGenerator = tokenGenerator;
        }

        public string Login(UserLoginDto dto)
        {
            var user =  repository.GetAllEntities()
                .FirstOrDefault(x => x.Email == dto.Email && x.Password == dto.Password);

               var token =  tokenGenerator.GenerateToken(user);
                return token;
        }

        public bool Register(UserDto dto)
        {
            bool result = false;
            var hashPassword = PasswordHassher.HashPassword(dto.Password);
            dto.Password = hashPassword;
            var user = repository.GetById(dto.Id);
            if (user == null)
            {
                Create(dto);
                result = true;
            }
            return result;
        }
    }
}
