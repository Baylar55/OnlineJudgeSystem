namespace AlgoCode.Application.Features.Contests.Commands.UpdateContest
{
    public class UpdateContestCommandValidator : AbstractValidator<UpdateContestCommand>
    {
        public UpdateContestCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("Title must not exceed 50 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .NotNull();

            RuleFor(p => p.StartTime)
                .NotEmpty().WithMessage("Start Time is required.")
                .NotNull();

            RuleFor(p => p.EndTime)
                .NotEmpty().WithMessage("End Time is required.")
                .NotNull();

            RuleFor(p => p.IsActive)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ProblemIds)
                .NotEmpty().WithMessage("Problem is required.")
                .NotNull();
        }
    }
}
