using AutoMapper;
using EasyTime.Application.Contract.Dtos;
using EasyTime.Application.Contract.IServices;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;
using EasyTime.Utilities.Convertor;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace EasyTime.Application.Services
{
    public class UserService : BaseService<UserDto, Guid, User>, IUserService
    {
        private readonly EmailService emailService = new EmailService();
        private readonly IBaseRepository<Guid, User> repository;
        private readonly ITokenGenerator tokenGenerator;
        public UserService(IMapper mapper, IBaseRepository<Guid, User> repository, ITokenGenerator tokenGenerator) : base(repository, mapper)
        {
            this.repository = repository;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<Result<string>> ChangePassword(string newPassword, Guid expireToken)
        {
            var users = await GetAll();
            var user = users.FirstOrDefault(x => x.TokenForChangePassword == expireToken);
            var tokenTime = DateTime.Now - user.ExpireChangePasswordToken;
            if (tokenTime < TimeSpan.FromMinutes(15))
            {
                user.TokenForChangePassword = null;
                user.ExpireChangePasswordToken = null;
               await Update(user);
                return Result<string>.Failure("expire Token Time");
            }
            else
            {
                newPassword = PasswordHassher.HashPassword(newPassword);
                user.Password = newPassword;
                user.TokenForChangePassword = null;
                user.ExpireChangePasswordToken = null;
               await Update(user);
                return Result<string>.Success("Password Changed Success");
            }
        }

        public async Task<Result<bool>> ForgotPassword(UserDto dto)
        {
            var users = await GetAll();
            var user = users.FirstOrDefault(x => x.Email == dto.Email && x.Password == dto.Password);

            if (user != null)
            {
                dto.TokenForChangePassword = Guid.NewGuid();
                dto.ExpireChangePasswordToken = DateTime.Now.AddMinutes(15);
                dto.Id = user.Id;
                string changePasswordUrl = $"http://localhost:5107/User/ChangePassword/{dto.TokenForChangePassword}";
                //emailService.SendEmailAsync(user.Email, "تغییر رمز عبور",changePasswordUrl);
               await Update(dto);
                return Result<bool>.Success(true, "email send success");
            }
            return Result<bool>.Failure("user Not Found ");
        }

        public async Task<Result<string>> Login(UserLoginDto dto)
        {
            var users = await repository.GetAllEntities();
                var user = users.FirstOrDefault(x => x.Email == dto.Email && x.Password == dto.Password);
            if (user != null)
            {
                var token = await tokenGenerator.GenerateToken(user);
                return Result<string>.Success($" Token Is : {token},Register Success");
            }
            return Result<string>.Failure("User Not Found ");
        }

        public async Task<bool> Register(UserDto dto)
        {
            var users = await repository.GetAllEntities();
            bool result = false;
            dto.Id = Guid.NewGuid();
            dto.TokenForChangePassword = null;
            dto.ExpireChangePasswordToken = null;
            dto.IsDelete = false;
            dto.CreateObjectDate = DateTime.Now;
            dto.UpdateObjectDate = DateTime.Now;
            var hashPassword = PasswordHassher.HashPassword(dto.Password);
            dto.Password = hashPassword;
            var checkUser = users.Any(x => x.UserName == dto.UserName && x.Email == dto.Email);
            if (checkUser is false)
            {
               await Create(dto);
                result = true;
            }
            return result;
        }
    }

}
