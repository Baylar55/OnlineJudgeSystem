namespace AlgoCode.Application.Features.Solutions.Commands.UpdateSolution
{
    public class UpdateSolutionCommandHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateSolutionCommand, ValidationResultModel>
    {
        public async Task<ValidationResultModel> Handle(UpdateSolutionCommand request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var solution = await context.Solutions
                .Include(s => s.Submission)
                .Where(s => s.Submission.UserId == userId)
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken) ?? throw new NotFoundException(request.Id.ToString(), nameof(Solution));

            solution = request.Adapt(solution);

            await context.SaveChangesAsync(cancellationToken);

            return new ValidationResultModel();
        }
    }
}
