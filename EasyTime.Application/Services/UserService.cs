using AutoMapper;
using EasyTime.Application.Contract.Dtos;
using EasyTime.Application.Contract.IServices;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;
using EasyTime.Utilities.Convertor;

namespace EasyTime.Application.Services
{
    public class UserService : IUserService
    {
        private readonly EmailService emailService = new EmailService();
        private readonly IBaseRepository<Guid, User> repository;
        private readonly ITokenGenerator tokenGenerator;
        public UserService(IMapper mapper, IBaseRepository<Guid, User> repository, ITokenGenerator tokenGenerator)
        {
            this.repository = repository;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<Result<string>> ChangePassword(string newPassword, Guid expireToken)
        {
            var users = await repository.GetAllEntities();
            var user = users.FirstOrDefault(x => x.TokenForChangePassword == expireToken);
            var check = await CheckResetToken(expireToken);
            if (check.IsSuccess)
            {
                user.TokenForChangePassword = null;
                user.ExpireChangePasswordToken = null;
                await repository.Update(user);
                return Result<string>.Failure("expire Token Time");
            }
            else
            {
                newPassword = PasswordHassher.HashPassword(newPassword);
                user.Password = newPassword;
                user.TokenForChangePassword = null;
                user.ExpireChangePasswordToken = null;
                await repository.Update(user);
                return Result<string>.Success("Password Changed Success");
            }
        }

        public async Task<Result<string>> CheckResetToken(Guid TokenForChangePassword)
        {
            var users = await repository.GetAllEntities();
            var user = users.FirstOrDefault(x => x.TokenForChangePassword == TokenForChangePassword);
            var tokenTime = DateTime.Now - user.ExpireChangePasswordToken;
            if (tokenTime < TimeSpan.FromMinutes(15))
            {
                return Result<string>.Success(TokenForChangePassword.ToString(), "توکن معتبر است ");
            }
            else
            {
                return Result<string>.Failure("توکن نامعتبر است ");
            }
        }

        public async Task<Result<bool>> ForgotPassword(UserDto dto)
        {
            var users = await repository.GetAllEntities(); // GetAll باید AsNoTracking داشته باشه
            var user = users.FirstOrDefault(x => x.Email == dto.Email);

            if (user != null)
            {
                user.TokenForChangePassword = dto.TokenForChangePassword = Guid.NewGuid();
                user.ExpireChangePasswordToken = dto.ExpireChangePasswordToken = DateTime.Now.AddMinutes(15);
                user.Id = dto.Id;

                string changePasswordUrl = $"http://localhost:3000/ResetPasswordRouter?token={dto.TokenForChangePassword}";

                await emailService.SendEmailAsync(user.Email, "تغییر رمز عبور", changePasswordUrl);

                await repository.Update(user); // اینجا دیگه conflict پیش نمیاد چون کاربر از اول track نشده

                return Result<bool>.Success(true, "ایمیل با موفقیت ارسال شد");
            }

            return Result<bool>.Failure("کاربری با این ایمیل یافت نشد");
        }


        public async Task<Result<string>> Login(UserLoginDto dto)
        {
            var users = await repository.GetAllEntities();
            var user = users.FirstOrDefault(x => x.Email == dto.Email && x.Password == PasswordHassher.HashPassword(dto.Password));
            if (user != null)
            {
                var token = await tokenGenerator.GenerateToken(user);
                return Result<string>.Success(user.Id.ToString(), $" Token Is : {token},Register Success");
            }
            return Result<string>.Failure("User Not Found ");
        }

        public async Task<bool> Register(UserDto dto)
        {
            var users = await repository.GetAllEntities();
            var user = new User()
            {
                Id = dto.Id = Guid.NewGuid(),
                TokenForChangePassword = dto.TokenForChangePassword = null,
                ExpireChangePasswordToken = dto.ExpireChangePasswordToken = null,
                IsDelete = dto.IsDelete = false,
                CreateObjectDate = dto.CreateObjectDate = DateTime.Now,
                UpdateEntityDate = dto.UpdateObjectDate = DateTime.Now,
                Password = PasswordHassher.HashPassword(dto.Password)
            };
            bool result = false;
            var checkUser = users.Any(x => x.UserName == dto.UserName && x.Email == dto.Email);
            if (checkUser is false)
            {
                await repository.Add(user);
                result = true;
            }
            return result;
        }
    }

}
