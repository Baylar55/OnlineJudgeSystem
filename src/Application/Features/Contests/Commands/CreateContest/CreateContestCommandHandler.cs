namespace AlgoCode.Application.Features.Contests.Commands.CreateContest;

public class CreateContestCommandHandler(IApplicationDbContext context, IFileService fileService) : IRequestHandler<CreateContestCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(CreateContestCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResultModel();

        if (request.StartTime > request.EndTime)
        {
            validationResult.Errors.Add("StartTime", new(["Start time must be less than end time"]));
            return validationResult;
        }

        if (request.ProblemIds == null || !request.ProblemIds.Any())
        {
            validationResult.Errors.Add("ProblemIds", new(["Contest must have at least one problem"]));
            return validationResult;
        }

        if (request.Photo == null)
        {
            validationResult.Errors.Add("Photo", new(["Photo is required"]));
            return validationResult;
        }

        bool isExist = await context.Contests.AnyAsync(x => x.Title.Trim().ToLower() == request.Title.Trim().ToLower(), cancellationToken);
        
        if (isExist)
        {
            validationResult.Errors.Add(string.Empty, new(["Contest with this title already exists"]));
            return validationResult;
        }

        if (!fileService.IsImage(request.Photo))
        {
            validationResult.Errors.Add("Photo", new(["Photo must be an image"]));
            return validationResult;
        }

        var contest = request.Adapt<Contest>();
        contest.CoverPhoto = await fileService.UploadAsync(request.Photo);

        if (request.ProblemIds != null && request.ProblemIds.Count != 0)
            contest.Problems = await context.Problems.Where(p => request.ProblemIds.Contains(p.Id)).ToListAsync(cancellationToken);

        await context.Contests.AddAsync(contest, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return validationResult;
    }
}
