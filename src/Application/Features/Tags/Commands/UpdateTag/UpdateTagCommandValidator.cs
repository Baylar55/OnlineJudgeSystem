namespace AlgoCode.Application.Features.Tags.Commands.UpdateTag;

public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
{
    public UpdateTagCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Title is required.")
            .NotNull()
            .MaximumLength(70).WithMessage("Title must not exceed 70 characters.");
    }
}
