namespace AlgoCode.Application.Features.Problem.Commands.CreateProblem
{
    public class CreateProblemCommandValidator : AbstractValidator<CreateProblemCommand>
    {
        public CreateProblemCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotNull().WithMessage("Title is required")
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(3).WithMessage("Title must exceed 3 characters");

            RuleFor(v => v.Description)
                .NotNull().WithMessage("Description is required")
                .NotEmpty().WithMessage("Description is required");

            RuleFor(v => v.Difficulty)
                .NotNull().WithMessage("Difficulty is required")
                .NotEmpty().WithMessage("Difficulty is required");

            RuleFor(v => v.Point)
                .NotNull().WithMessage("Point is required")
                .NotEmpty().WithMessage("Point is required")
                .GreaterThan(0).WithMessage("Point must be greater than 0");

            RuleFor(v => v.AccessLevel)
                .NotNull().WithMessage("AccessLevel is required")
                .NotEmpty().WithMessage("AccessLevel is required");
        }
    }
}
