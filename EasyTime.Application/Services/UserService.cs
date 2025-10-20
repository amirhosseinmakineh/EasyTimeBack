using AutoMapper;
using EasyTime.Application.Contract.Dtos;
using EasyTime.Application.Contract.Dtos.Achevment;
using EasyTime.Application.Contract.Dtos.BusinesDto;
using EasyTime.Application.Contract.Dtos.BusinessOwnerDtos;
using EasyTime.Application.Contract.Dtos.Comments;
using EasyTime.Application.Contract.IServices;
using EasyTime.InfraStracure.UnitOfWork;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;
using EasyTime.Utilities.Convertor;
using Microsoft.EntityFrameworkCore;

namespace EasyTime.Application.Services
{
    public class UserService : IUserService, IService
    {
        private readonly EmailService emailService;
        private readonly IBaseRepository<Guid, User> repository;
        private readonly ITokenGenerator tokenGenerator;
        private readonly IBaseRepository<long, Business> businessRepository;
        private readonly IBaseRepository<long, BusinessDay> businessDayRepository;
        private readonly IBaseRepository<long, BusinessTime> businessTimeRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UserService(IMapper mapper, IBaseRepository<Guid, User> repository, ITokenGenerator tokenGenerator, IBaseRepository<long, Business> businessRepository, IBaseRepository<long, BusinessDay> businessDayRepository, IBaseRepository<long, BusinessTime> businessTimeRepository, IUnitOfWork unitOfWork, EmailService emailService)
        {
            this.repository = repository;
            this.tokenGenerator = tokenGenerator;
            this.mapper = mapper;
            this.businessRepository = businessRepository;
            this.businessDayRepository = businessDayRepository;
            this.businessTimeRepository = businessTimeRepository;
            this.unitOfWork = unitOfWork;
            this.emailService = emailService;
        }

        public async Task<Result<string>> ChangePassword(string newPassword, Guid expireToken)
        {
            var users = repository.GetAllEntities();
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
                var hashed = PasswordHasher.HashPassword(newPassword, out var salt);
                user.Password = hashed;
                user.PasswordSalt = salt;
                user.TokenForChangePassword = null;
                user.ExpireChangePasswordToken = null;
                await repository.Update(user);
                return Result<string>.Success("Password Changed Success");
            }
        }

        public async Task<Result<string>> CheckResetToken(Guid TokenForChangePassword)
        {
            var users = repository.GetAllEntities();
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
            var users = repository.GetAllEntities(); // GetAll باید AsNoTracking داشته باشه
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

        public async Task<Result<BusinessOwnerProfileDto>> GetBusinessOwnerProfile(Guid id)
        {
            var users = repository.GetAllEntities();
            var profile = await users
            .AsNoTracking()
            .Where(x => x.Id == id && x.IsBusinesOwner)
            .Select(x => new BusinessOwnerProfileDto
            {
                Id = x.Id,
                Name = x.UserName,
                Banner = x.Banner,
                AboutMe = x.AboutMe,
                Family = x.Family,
                ImageName = x.ImageName,

                Achievements = x.Achievements.Select(a => new AchievementDto
                {
                    Name = a.Name,
                    UserId = a.UserId
                }).ToList(),

                BusinessInfo = x.Businesses
                .Select(b => new BusinessDto
                {
                    BusinessName = b.Name,
                    Description = b.Description
                })
                .FirstOrDefault(),

                WorkHistory = x.WorkHistory
            })
                .FirstOrDefaultAsync();

            var comments = await users
            .AsNoTracking()
            .Where(b => b.Id == id)
            .SelectMany(b => b.Businesses)
            .SelectMany(b => b.Comments)
            .Select(c => new CommentDto
            {
                CommentId = c.Id,
                BusinessId = c.BusinessId,
                CommentText = c.CommentText,
                UserId = c.UserId
            })
            .ToListAsync();

            var takingTurns = await users
                .AsNoTracking()
                .Where(u => u.Id == id && u.IsBusinesOwner)
                .SelectMany(u => u.Businesses, (u, b) => new { u, b })
                .SelectMany(x => x.b.BusinessDays, (x, d) => new { x.u, x.b, d })
                .SelectMany(
                    x => x.d.BusinessTimes.DefaultIfEmpty(),
                    (x, bt) => new BusinessDayTimeDto
                    {
                        BusinessOwnerDayId = x.d.BusinessOwnerDay.Id,
                        BusinessOwnerTimeId = bt != null ? bt.BusinessOwnerTime.Id : (long?)null,
                        DayOfWeek = x.d.BusinessOwnerDay.DayOfWeek,
                        From = bt.BusinessOwnerTime.From,
                        To = bt.BusinessOwnerTime.To,
                        IsReserved = bt.BusinessOwnerTime.IsReserved
                    }
                )
                .ToListAsync();


            if (profile != null)
            {
                profile.Comments = comments;
                profile.TakingTurns = takingTurns;
            }
            return Result<BusinessOwnerProfileDto>.Success(profile);
        }

        public async Task<Result<string>> Login(UserLoginDto dto)
        {
            var users = repository.GetAllEntities();
            var user = users.FirstOrDefault(x => x.Email == dto.Email);
            if (user != null && PasswordHasher.VerifyPassword(dto.Password, user.PasswordSalt, user.Password))
            {
                var token = await tokenGenerator.GenerateToken(user);
                return Result<string>.Success(user.Id.ToString(), $" Token Is : {token},Register Success");
            }
            return Result<string>.Failure("User Not Found ");
        }

        public async Task<bool> Register(UserDto dto)
        {
            bool result = false;

            var users = repository.GetAllEntities();
            var hashedPassword = PasswordHasher.HashPassword(dto.Password, out var salt);
            var user = new User()
            {
                Id = dto.Id = Guid.NewGuid(),
                TokenForChangePassword = dto.TokenForChangePassword = null,
                ExpireChangePasswordToken = dto.ExpireChangePasswordToken = null,
                IsDelete = dto.IsDelete = false,
                CreateObjectDate = dto.CreateObjectDate = DateTime.Now,
                UpdateEntityDate = dto.UpdateObjectDate = DateTime.Now,
                Password = hashedPassword,
                PasswordSalt = salt,
                Email = dto.Email,
                RoleId = 3,
                IsProfileComplete = false,
                AboutMe = null
            };
            if (user.IsBusinesOwner == true)
            {
                user.RoleId = 2;
            }

            var checkUser = users.Any(x => x.UserName == dto.UserName && x.Email == dto.Email);

            if (checkUser is false)
            {
                await repository.Add(user);
                await repository.SaveChanges();
                result = true;
            }
            return result;
        }
        public async Task<Result<object>> UpdateBusinessOwnerInfo(UpdateBusinessOwnerInfoDto dto)
        {
            await unitOfWork.Begin();

            try
            {
                var user = await repository.GetById(dto.Id);
                if (user == null)
                    return Result<object>.Failure("User not found");

                user.Age = dto.Age;
                user.Family = dto.Family;
                user.ImageName = dto.ImageName;
                await repository.Update(user);

                var business = new Business
                {
                    CityId = dto.City.Id,
                    RegionId = dto.Region.Id,
                    NeighberhoodId = dto.Neighborhood.Id,
                    Name = dto.Business.BusinessName,
                    CreateObjectDate = DateTime.UtcNow,
                    Description = dto.Business.Description,
                    IsDelete = false,
                    Logo = dto.Business.BusinessLogo,
                    BusinessOwnerId = user.Id,

                };
                await businessRepository.Add(business);
                await businessRepository.SaveChanges();

                var businessDays = dto.DayIdes
                    .Select(dayId => new BusinessDay
                    {
                        BusinessId = business.Id,
                        DayId = dayId,
                        CreateObjectDate = DateTime.UtcNow
                    }).ToList();

                await businessDayRepository.AddRange(businessDays);
                await businessDayRepository.SaveChanges();

                var businessTimes = businessDays
                    .SelectMany(d => dto.TimeIdes.Select(timeId => new BusinessTime
                    {
                        BusinessDayId = d.Id,
                        TimeId = timeId
                    })).ToList();

                await businessTimeRepository.AddRange(businessTimes);
                await businessTimeRepository.SaveChanges();

                await unitOfWork.Commit();
                return Result<object>.Success(user, "Update Successfully");
            }
            catch (Exception ex)
            {
                await unitOfWork.RoleBack();
                return Result<object>.Failure("Update Failed: " + ex.Message);
            }
        }

    }

}
