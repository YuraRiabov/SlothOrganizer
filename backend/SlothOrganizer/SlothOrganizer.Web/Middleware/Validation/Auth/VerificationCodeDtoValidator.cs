using FluentValidation;
using SlothOrganizer.Contracts.DTO.Auth;

namespace SlothOrganizer.Web.Middleware.Validation.Auth
{
    public class VerificationCodeDtoValidator : AbstractValidator<VerificationCodeDto>
    {
        public VerificationCodeDtoValidator()
        {
            RuleFor(x => x.VerificationCode)
                .NotEmpty()
                .GreaterThan(99_999)
                .LessThan(1_000_000);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
