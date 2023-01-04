using FluentValidation;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;

namespace SlothOrganizer.Web.Middleware.Validation.Tasks
{
    public class NewTaskDtoValidator : AbstractValidator<NewTaskDto>
    {
        public NewTaskDtoValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty()
                .MinimumLength(2)
                .MinimumLength(30);

            RuleFor(t => t.Description).MaximumLength(200);

            RuleFor(t => t.RepeatingPeriod).IsInEnum();

            RuleFor(t => t.End).Must((task, end) => end > task.Start);

            RuleFor(t => t.EndRepeating).Must((task, end) => task.RepeatingPeriod == TaskRepeatingPeriod.None || end > task.End);
        }
    }
}
