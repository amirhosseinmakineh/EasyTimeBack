using EasyTime.Application.Contract.Dtos;
using FluentValidation;

namespace EasyTime.Application.Validator
{
        public class ForgotPasswordValidation : AbstractValidator<ForgotPasswordDto>
        {
            public ForgotPasswordValidation()
            {
                RuleFor(x => x.Email)
               .EmailAddress()
               .WithMessage("Invalid Email")
               .NotEmpty()
         .WithMessage("Email Can Not Be Empty");
            }
        }
    }
