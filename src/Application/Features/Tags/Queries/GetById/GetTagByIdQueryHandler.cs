namespace AlgoCode.Application.Features.Tags.Queries.GetById;

public class GetTagByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTagByIdQuery, GetTagByIdQueryResponse>
{
    public async Task<GetTagByIdQueryResponse> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Tags.FindAsync([request.Id], cancellationToken) ?? throw new NotFoundException(request.Id.ToString(), nameof(Tag));

        return entity.Adapt<GetTagByIdQueryResponse>();
    }
}
