namespace AlgoCode.Application.Features.TestCases.Queries.GetById;

public record GetTestCaseByIdQuery(int Id) : IRequest<GetTestCaseByIdQueryResponse>;

public class GetTestCaseByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTestCaseByIdQuery, GetTestCaseByIdQueryResponse>
{
    public async Task<GetTestCaseByIdQueryResponse> Handle(GetTestCaseByIdQuery request, CancellationToken cancellationToken)
    {
        var testCase = await context.TestCases.FindAsync([request.Id], cancellationToken) ?? throw new NotFoundException(request.Id.ToString(), nameof(TestCase));

        return testCase.Adapt<GetTestCaseByIdQueryResponse>();
    }
}
