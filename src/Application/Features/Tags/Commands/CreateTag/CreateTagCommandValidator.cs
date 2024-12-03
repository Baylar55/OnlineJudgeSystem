namespace AlgoCode.Application.Features.Tags.Commands.CreateTag;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotNull()
            .NotEmpty().WithName("Tag title required");
    }
}
