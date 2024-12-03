namespace AlgoCode.Application.Features.Solutions.Commands.PostSolution;

public class PostSolutionCommandHandler(IApplicationDbContext context) : IRequestHandler<PostSolutionCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(PostSolutionCommand request, CancellationToken cancellationToken)
    {
        var submission = await context.Submissions
                                      .Include(s => s.Problem)
                                      .Include(s => s.User)
                                      .FirstOrDefaultAsync(s => s.Id == request.SubmissionId, cancellationToken) 
                                      ?? throw new NotFoundException(request.SubmissionId.ToString(), nameof(Submission));

        var solution = request.Adapt<Solution>();
        solution.SubmissionId = submission.Id;

        await context.Solutions.AddAsync(solution, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
        
        return new ValidationResultModel();
    }
}
