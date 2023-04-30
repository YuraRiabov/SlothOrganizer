using FluentValidation;
using SlothOrganizer.Contracts.DTO.Tasks.Dashboard;

namespace SlothOrganizer.Web.Middleware.Validation.Tasks
{
    public class NewDashboardDtoValidator : AbstractValidator<NewDashboardDto>
    {
        public NewDashboardDtoValidator()
        {
            RuleFor(d => d.Title).NotEmpty();
        }
    }
}
