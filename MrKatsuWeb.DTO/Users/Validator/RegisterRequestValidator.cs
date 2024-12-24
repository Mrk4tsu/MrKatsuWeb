using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrKatsuWeb.DTO.Users.Validator
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty()
                .MaximumLength(50).WithMessage("Fullname can not over 50 character");
            RuleFor(x => x.FullName).NotEmpty()
                .MaximumLength(150).WithMessage("Fullname can not over 150 character");          
            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress().WithMessage("Email invalid");
            RuleFor(x => x.Password).NotEmpty()
               .MinimumLength(6).WithMessage("Password is at least 6 character ")
               .Matches(@"^\S*$").WithMessage("Password cannot have space");
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Confirm password not match");
                }
            });
        }
    }
}
