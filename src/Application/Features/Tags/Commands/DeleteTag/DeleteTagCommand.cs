using Ardalis.GuardClauses;

namespace AlgoCode.Application.Features.Tags.Commands.DeleteTag;

public record DeleteTagCommand(int Id) : IRequest;

public class DeleteTagCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteTagCommand>
{
    public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Tags.FindAsync([request.Id], cancellationToken: cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);

        context.Tags.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
