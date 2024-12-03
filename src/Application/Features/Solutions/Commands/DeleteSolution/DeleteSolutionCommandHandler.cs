namespace AlgoCode.Application.Features.Solutions.Commands.DeleteSolution;

public class DeleteSolutionCommandHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IRequestHandler<DeleteSolutionCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(DeleteSolutionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResultModel();

        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        var solution = await context.Solutions
            .Include(s => s.Submission)
            .Where(s => s.Submission.UserId == userId)
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken) ?? throw new NotFoundException(request.Id.ToString(), nameof(Solution));

        context.Comments.RemoveRange(context.Comments.Where(c => c.SolutionId == solution.Id));
        
        context.Solutions.Remove(solution);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return validationResult;
    }
}
