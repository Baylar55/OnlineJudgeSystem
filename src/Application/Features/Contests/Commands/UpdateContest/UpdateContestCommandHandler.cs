namespace AlgoCode.Application.Features.Contests.Commands.UpdateContest;

public class UpdateContestCommandHandler(IApplicationDbContext context, IFileService fileService) : IRequestHandler<UpdateContestCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(UpdateContestCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResultModel();
        var contest = context.Contests.Include(x => x.Problems).FirstOrDefault(x => x.Id == request.Id);

        if (contest == null)
        {
            validationResult.Errors.Add("", ["Contest not found."]);
            return validationResult;
        }

        if (request.StartTime > request.EndTime)
            validationResult.Errors.Add("StartTime", new(["Start time must be less than end time"]));

        if (await context.Contests.AnyAsync(x => x.Title.Trim().ToLower() == request.Title.Trim().ToLower(), cancellationToken))
            validationResult.Errors.Add("Title", new(["Contest with this title already exists"]));

        request.Adapt(contest);

        if (request.Photo != null)
        {
            if (!fileService.IsImage(request.Photo))
                validationResult.Errors.Add("Photo", new(["Photo must be an image"]));

            fileService.Delete(contest.CoverPhoto);

            contest.CoverPhoto = await fileService.UploadAsync(request.Photo);
        }

        if (!validationResult.IsValid) return validationResult;

        contest.Problems.Clear();

        if (request.ProblemIds != null && request.ProblemIds.Any())
            contest.Problems = await context.Problems.Where(p => request.ProblemIds.Contains(p.Id)).ToListAsync(cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return validationResult;
    }
}
