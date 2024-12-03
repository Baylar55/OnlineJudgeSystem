namespace AlgoCode.Application.Features.Problem.Queries.GetRandom;

public record GetRandomProblemQuery(): IRequest<int>;

public class GetRandomProblemQueryHandler(IApplicationDbContext context) : IRequestHandler<GetRandomProblemQuery, int>
{
    public async Task<int> Handle(GetRandomProblemQuery request, CancellationToken cancellationToken)
    {
        var problems = await context.Problems.ToListAsync(cancellationToken);

        var random = new Random();

        var index = random.Next(0, problems.Count);

        return problems[index].Id;
    }
}
