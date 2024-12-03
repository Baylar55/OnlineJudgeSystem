namespace AlgoCode.Application.Features.Sessions.Queries.GetById;

public record GetSessionByIdQuery(int Id) : IRequest<GetSessionByIdQueryResponse>;

public record GetSessionByIdQueryResponse(int Id, string Title, bool IsActive, string UserId, DateTimeOffset Created, DateTimeOffset LastModified);

public class GetSessionByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetSessionByIdQuery, GetSessionByIdQueryResponse>
{
    public async Task<GetSessionByIdQueryResponse> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
    {
        var session = await context.Sessions.FindAsync([request.Id], cancellationToken) ?? throw new NotFoundException(nameof(Session), request.Id.ToString());

        return session.Adapt<GetSessionByIdQueryResponse>();
    }
}
