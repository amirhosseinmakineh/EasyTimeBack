using EasyTime.Application.Contract.Dtos;
using FluentValidation;

namespace EasyTime.Application.Validator
{
    public  class RegisterUserValidation : AbstractValidator<UserDto>
    {
        public RegisterUserValidation()
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
