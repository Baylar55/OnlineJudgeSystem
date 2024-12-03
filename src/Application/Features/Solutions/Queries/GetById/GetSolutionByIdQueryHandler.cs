namespace AlgoCode.Application.Features.Solutions.Queries.GetById;

public class GetSolutionByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetSolutionByIdQuery, GetSolutionByIdQueryResponse>
{
    public async Task<GetSolutionByIdQueryResponse> Handle(GetSolutionByIdQuery request, CancellationToken cancellationToken)
    {
        var solution = await context.Solutions
                                     .Include(x => x.Submission)
                                     .ThenInclude(x => x.User)
                                     .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                     ?? throw new NotFoundException(request.Id.ToString(), nameof(Solution));

        return solution.Adapt<GetSolutionByIdQueryResponse>();
    }
}
