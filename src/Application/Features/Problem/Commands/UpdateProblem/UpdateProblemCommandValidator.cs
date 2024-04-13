namespace AlgoCode.Application.Features.Problem.Commands.UpdateProblem
{
    public class UpdateProblemCommandValidator : AbstractValidator<UpdateProblemCommand>
    {
        public UpdateProblemCommandValidator()
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
                .NotEmpty().WithMessage("Point is required");
        }
    }
}
