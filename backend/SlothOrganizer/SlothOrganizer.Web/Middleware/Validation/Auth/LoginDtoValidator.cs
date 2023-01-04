using System.Text.RegularExpressions;
using FluentValidation;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.Validation;

namespace SlothOrganizer.Web.Middleware.Validation.Auth
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(16)
                .Must(p => p.IsValidPassword());
        }
    }
}
