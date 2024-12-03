namespace AlgoCode.Application.Features.TestCases.Commands.UpdateTestCase;

public class UpdateTestCaseCommandValidator : AbstractValidator<UpdateTestCaseCommand>
{
    public UpdateTestCaseCommandValidator()
    {
        RuleFor(p => p.ProblemId)
            .NotEmpty().WithMessage("Problem is required.")
            .NotNull();

        RuleFor(p => p.Inputs)
            .NotEmpty().WithMessage("Input is required.")
            .NotNull();

        RuleFor(p => p.ExpectedOutput)
            .NotEmpty().WithMessage("Expected Output is required.")
            .NotNull();
    }
}
