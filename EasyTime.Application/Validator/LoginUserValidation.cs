using EasyTime.Application.Contract.Dtos;
using FluentValidation;

namespace EasyTime.Application.Validator
{
        public  class LoginUserValidation : AbstractValidator<UserLoginDto>
        {
            public LoginUserValidation()
            {
                RuleFor(x => x.Email)
               .EmailAddress()
               .WithMessage("Invalid Email")
               .NotEmpty()
               .WithMessage("Email Can Not Be Empty");

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("Password Can Not Be Empty");
            }
        }
    }
