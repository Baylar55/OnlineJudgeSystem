namespace AlgoCode.Application.Features.Tags.Queries.GetAll;

public record GetAllTagsQuery() : IRequest<GetAllTagsQueryResponse>;

public record GetAllTagsQueryResponse(List<Tag> Tags);

public class GetAllTagsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllTagsQuery, GetAllTagsQueryResponse>
{
    public async Task<GetAllTagsQueryResponse> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await context.Tags.ToListAsync(cancellationToken);

        return new GetAllTagsQueryResponse(tags);
    }
}
