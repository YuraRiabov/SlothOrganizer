using FluentValidation;
using SlothOrganizer.Contracts.DTO.Tasks.Task;

namespace SlothOrganizer.Web.Middleware.Validation.Tasks
{
    public class TaskCompletionDtoValidator : AbstractValidator<TaskCompletionDto>
    {
        public TaskCompletionDtoValidator()
        {
            RuleFor(taskCompletion => taskCompletion.End)
                .Must((taskCompletion, end) => end > taskCompletion.Start);
        }
    }
}
