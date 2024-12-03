using AlgoCode.Application.DTOs.Session;

namespace AlgoCode.Application.Features.Sessions.Queries.GetById;

public record GetSessionDetailsByIdQuery() : IRequest<GetSessionDetailsByIdQueryResponse?>;

public record GetSessionDetailsByIdQueryResponse(int Id, string Title, int AllProblemsCount, int SolvedProblemsCount,
    int AllEasyProblemsCount, int AllMediumProblemsCount, int AllHardProblemsCount, int SolvedEasyProblemsCount, int SolvedMediumProblemsCount, int SolvedHardProblemsCount);

public class GetSessionDetailsByIdQueryHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetSessionDetailsByIdQuery, GetSessionDetailsByIdQueryResponse?>
{
    public async Task<GetSessionDetailsByIdQueryResponse?> Handle(GetSessionDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return null;

        var session = await context.Sessions
                                    .Where(s => s.UserId == userId)
                                    .Include(s => s.Submissions)
                                    .ThenInclude(sub => sub.Problem)
                                    .ThenInclude(p => p.UserProblemStatuses)
                                    .FirstOrDefaultAsync(s => s.IsActive, cancellationToken) ?? throw new NotFoundException("Active not found", nameof(Session));

        var problemCounts = await GetProblemCountsAsync(session.Id, cancellationToken);

        var response = problemCounts.Adapt<GetSessionDetailsByIdQueryResponse>() with
        {
            Id = session.Id,
            Title = session.Title
        };

        return response;
    }

    private async Task<ProblemCountsForSessionDTO> GetProblemCountsAsync(int sessionId, CancellationToken cancellationToken)
    {
        var userProblemStatuses = await context.UserProblemStatuses
                                               .Where(ups => ups.SessionId == sessionId)
                                               .Include(ups => ups.Problem)
                                               .ToListAsync(cancellationToken);

        var solvedProblemsCount = userProblemStatuses.Count(u => u.Status == ProblemStatus.Solved);

        var solvedEasyProblemsCount = userProblemStatuses.Count(u => u.Status == ProblemStatus.Solved && u.Problem.Difficulty == ProblemDifficulty.Easy);
        var solvedMediumProblemsCount = userProblemStatuses.Count(u => u.Status == ProblemStatus.Solved && u.Problem.Difficulty == ProblemDifficulty.Medium);
        var solvedHardProblemsCount = userProblemStatuses.Count(u => u.Status == ProblemStatus.Solved && u.Problem.Difficulty == ProblemDifficulty.Hard);

        var allProblemsCount = await context.Problems.CountAsync(cancellationToken);
        var allEasyProblemsCount = await context.Problems.CountAsync(p => p.Difficulty == ProblemDifficulty.Easy, cancellationToken);
        var allMediumProblemsCount = await context.Problems.CountAsync(p => p.Difficulty == ProblemDifficulty.Medium, cancellationToken);
        var allHardProblemsCount = await context.Problems.CountAsync(p => p.Difficulty == ProblemDifficulty.Hard, cancellationToken);

        return new ProblemCountsForSessionDTO(allProblemsCount, solvedProblemsCount, allEasyProblemsCount, allMediumProblemsCount, allHardProblemsCount, solvedEasyProblemsCount, solvedMediumProblemsCount, solvedHardProblemsCount);
    }
}

