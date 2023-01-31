using FluentValidation;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Contracts.Validation;

namespace SlothOrganizer.Web.Middleware.Validation.Users
{
    public class PasswordUpdateDtoValidator : AbstractValidator<PasswordUpdateDto>
    {
        public PasswordUpdateDtoValidator()
        {
            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(16)
                .Must(p => p.IsValidPassword());
        }
    }
}
