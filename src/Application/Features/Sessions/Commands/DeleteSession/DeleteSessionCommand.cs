namespace AlgoCode.Application.Features.Sessions.Commands.DeleteSession;

public record DeleteSessionCommand(int Id) : IRequest<ValidationResultModel>;

public class DeleteSessionCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteSessionCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
    {
        var sessions = await context.Sessions.ToListAsync(cancellationToken);

        var validationResult = new ValidationResultModel();

        if (sessions.Count == 1)
        {
            validationResult.Errors.Add("Session", ["You can't delete the last session"]);
            return validationResult;
        }
        else
        {
            var session = await context.Sessions.FindAsync([request.Id], cancellationToken: cancellationToken);
            if (session == null)
            {
                validationResult.Errors.Add("Session", ["Session not found"]);
                return validationResult;
            }

            context.Sessions.Remove(session);
            await context.SaveChangesAsync(cancellationToken);
        }
        return validationResult;
    }
}
