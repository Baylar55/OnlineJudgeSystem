namespace AlgoCode.Application.Features.Solutions.Commands.PostSolution;

public class PostSolutionCommandValidator : AbstractValidator<PostSolutionCommand>
{
    public PostSolutionCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Title is required.")
            .NotNull()
            .MaximumLength(60).WithMessage("Title must not exceed 60 characters.");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required.")
            .NotNull()
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters."); 
    }
}
