using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Application.Features.Contests.Queries.GetById;

public record GetContestByIdQuery(int Id) : IRequest<GetContestByIdQueryResponse>;

public record GetContestByIdQueryResponse(int Id, string Title, string Description, bool IsActive, string Photo, 
    DateTime StartTime, DateTime EndTime, DateTimeOffset Created, string? CreatedBy, DateTimeOffset? LastModified, string? LastModifiedBy, List<Domain.Entities.Problem> Problems);

public class GetContestByIdQueryHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager) : IRequestHandler<GetContestByIdQuery, GetContestByIdQueryResponse>
{
    public async Task<GetContestByIdQueryResponse> Handle(GetContestByIdQuery request, CancellationToken cancellationToken)
    {
        var contest = await context.Contests.Include(c => c.Problems)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken) ?? throw new NotFoundException(request.Id.ToString(),nameof(Contest));

        var createdBy = await userManager.FindByIdAsync(contest.CreatedBy!);
        
        var lastModifiedBy = await userManager.FindByIdAsync(contest.LastModifiedBy!);

        return contest.Adapt<GetContestByIdQueryResponse>() with
        {
            CreatedBy = createdBy?.UserName,
            LastModifiedBy = lastModifiedBy?.UserName
        };
    }
}
