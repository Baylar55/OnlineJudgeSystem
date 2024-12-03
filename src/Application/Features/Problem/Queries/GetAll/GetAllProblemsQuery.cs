namespace AlgoCode.Application.Features.Problem.Queries.GetAll;

public record GetAllProblemsQuery() : IRequest<List<Domain.Entities.Problem>>;

public class GetAllProblemsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllProblemsQuery, List<Domain.Entities.Problem>>
{
    public async Task<List<Domain.Entities.Problem>> Handle(GetAllProblemsQuery request, CancellationToken cancellationToken)
    {
        return await context.Problems.ToListAsync(cancellationToken);
    }
}
