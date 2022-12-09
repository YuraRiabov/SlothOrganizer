using System.Text.RegularExpressions;
using FluentValidation;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Web.Middleware.Validation.Users
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(16)
                .Must(p => Regex.IsMatch(p, "([0-9]+[a-zA-Z]+[0-9a-zA-Z]*)|([a-zA-Z]+[0-9]+[0-9a-zA-Z]*)"));
        }
    }
}
