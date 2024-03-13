namespace AlgoCode.Application.Features.Tags.Commands.CreateTag
{
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        private readonly IApplicationDbContext context;

        public CreateTagCommandValidator(IApplicationDbContext context)
        {
            this.context = context;

            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotNull()
                .NotEmpty().WithName("Tag title");
        }
    }
}
