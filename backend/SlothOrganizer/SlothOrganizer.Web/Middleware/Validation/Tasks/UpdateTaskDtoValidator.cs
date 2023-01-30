using FluentValidation;
using SlothOrganizer.Contracts.DTO.Tasks.Task;

namespace SlothOrganizer.Web.Middleware.Validation.Tasks
{
    public class UpdateTaskDtoValidator : AbstractValidator<UpdateTaskDto>
    {
        public UpdateTaskDtoValidator()
        {
            RuleFor(t => t.Task.Title)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(60);

            RuleFor(t => t.Task.Description).MaximumLength(400);

            RuleFor(t => t.TaskCompletion.End).Must((task, end) => end > task.TaskCompletion.Start);
        }
    }
}
