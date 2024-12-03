namespace AlgoCode.Application.Features.Problem.Queries.GetById;

public record GetProblemByIdQuery(int Id) : IRequest<GetProblemByIdQueryResponse>;

public class GetProblemByIdQueryHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager) : IRequestHandler<GetProblemByIdQuery, GetProblemByIdQueryResponse>
{
    public async Task<GetProblemByIdQueryResponse> Handle(GetProblemByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Problems.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) 
            ?? throw new NotFoundException(nameof(Problem), request.Id.ToString());
        
        var createdBy = await userManager.FindByIdAsync(entity.CreatedBy!);
        var lastModifiedBy = await userManager.FindByIdAsync(entity.LastModifiedBy!);

        var response = entity.Adapt<GetProblemByIdQueryResponse>();

        return response with
        {
            CreatedBy = createdBy?.UserName,
            LastModifiedBy = lastModifiedBy?.UserName
        };
    }
}
