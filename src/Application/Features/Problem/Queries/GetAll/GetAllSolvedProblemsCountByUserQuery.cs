namespace AlgoCode.Application.Features.Problem.Queries.GetAll;

public record GetAllSolvedProblemsCountByUserQuery() : IRequest<int>;

public class GetAllSolvedProblemsCountByUserQueryHandler(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager, IApplicationDbContext context) : IRequestHandler<GetAllSolvedProblemsCountByUserQuery, int>
{
    public async Task<int> Handle(GetAllSolvedProblemsCountByUserQuery request, CancellationToken cancellationToken)
    {
        var userId = accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await userManager.FindByIdAsync(userId!);
        
        var problems = await context.Problems
                             .Where(p => p.Submissions!.Any(s => s.UserId == user!.Id && s.Status == SubmissionStatus.Accepted))
                             .Distinct()
                             .CountAsync(cancellationToken);
        return problems;
    }
}
