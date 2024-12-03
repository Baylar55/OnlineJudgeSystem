namespace AlgoCode.Application.Features.Contests.Commands.CreateContest;

public class CreateContestCommandValidator : AbstractValidator<CreateContestCommand>
{
    public CreateContestCommandValidator()
    {
        RuleFor(x => x.Title).MinimumLength(5).MaximumLength(50).NotEmpty().WithMessage("Title must be between 5 and 50 characters");
        RuleFor(x => x.Description).MinimumLength(5).NotEmpty().WithMessage("Description must be at least 5 characters");
        RuleFor(x => x.StartTime).NotEmpty().WithMessage("Start time is required");
        RuleFor(x => x.EndTime).NotEmpty().WithMessage("End time is required");
        RuleFor(x => x.ProblemIds).NotNull().WithMessage("Contest must have at least one problem");
        RuleFor(x => x.Photo).NotNull().WithMessage("Photo is required");
    }
}
