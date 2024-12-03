namespace AlgoCode.Application.Features.Sessions.Commands.CreateSession;

public record CreateSessionCommand(string Title, string UserId, bool IsActive) : IRequest<ValidationResultModel>;

public class CreateSessionCommandHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateSessionCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResultModel();

        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        int sessionCount = await context.Sessions.CountAsync(x => x.UserId == userId, cancellationToken);

        if (sessionCount >= 5)
        {
            validationResult.Errors.Add("Session", ["You can't create more than 5 sessions"]);
            return validationResult;
        }

        bool isExist = await context.Sessions.AnyAsync(x => x.Title.Trim().ToLower() == request.Title.Trim().ToLower(), cancellationToken);

        if (isExist)
        {
            validationResult.Errors.Add("Title", ["Session with this title already exists"]);
            return validationResult;
        }

        Session session = request.Adapt<Session>();
        session.UserId = userId!;

        await context.Sessions.AddAsync(session, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return validationResult;
    }
}
