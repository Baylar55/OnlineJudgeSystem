namespace AlgoCode.Application.Features.Subscriptions.Commands.UpdateSubscription;

public class UpdateSubscriptionCommandValidator : AbstractValidator<UpdateSubscriptionCommand>
{
    public UpdateSubscriptionCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Title is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("Title must not exceed 50 characters.");
        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required.")
            .NotNull();
        RuleFor(p => p.Duration)
            .NotEmpty().WithMessage("Duration is required.")
            .NotNull();
        RuleFor(p => p.Price)
            .NotEmpty().WithMessage("Price is required.")
            .NotNull();
    }
}
