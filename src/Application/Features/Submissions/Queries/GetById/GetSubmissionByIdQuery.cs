namespace AlgoCode.Application.Features.Submissions.Queries.GetById;

public record GetSubmissionByIdQuery(int Id) : IRequest<GetSubmissionByIdQueryResponse>;

public record GetSubmissionByIdQueryResponse(int Id, DateTimeOffset Created, string ProblemName, string SourceCode,
    string Language, string Status, DateTimeOffset SubmissionTime, double MemoryUsage, long ExecutionTime);


public class GetSubmissionByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetSubmissionByIdQuery, GetSubmissionByIdQueryResponse>
{
    public async Task<GetSubmissionByIdQueryResponse> Handle(GetSubmissionByIdQuery request, CancellationToken cancellationToken)
    {
        var submission = await context.Submissions.Include(x => x.Problem).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                              ?? throw new NotFoundException(request.Id.ToString(), nameof(Submission));

        return submission.Adapt<GetSubmissionByIdQueryResponse>();
    }
}
