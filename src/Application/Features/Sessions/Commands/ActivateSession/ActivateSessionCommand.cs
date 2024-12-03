namespace AlgoCode.Application.Features.Sessions.Commands.ActivateSession;

public record ActivateSessionCommand(int SessionId) : IRequest<ValidationResultModel>;

public class ActivateSessionCommandHandler(IApplicationDbContext context) : IRequestHandler<ActivateSessionCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(ActivateSessionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResultModel();

        var sessionToActivate = await context.Sessions.FindAsync([request.SessionId], cancellationToken: cancellationToken) ?? throw new NotFoundException(request.SessionId.ToString(), nameof(Session));

        var activeSession = await context.Sessions.FirstOrDefaultAsync(x => x.IsActive, cancellationToken);

        if (activeSession != null) activeSession.IsActive = false;

        sessionToActivate.IsActive = true;

        await context.SaveChangesAsync(cancellationToken);

        return validationResult;
    }
}
