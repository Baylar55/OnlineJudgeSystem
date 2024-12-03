namespace AlgoCode.Application.Features.Contests.Commands.UpdateContest;

public record UpdateContestCommand(int Id, string Title, string Description, IFormFile? Photo, string? PhotoName, DateTime StartTime, DateTime EndTime, bool IsActive, List<int> ProblemIds) : IRequest<ValidationResultModel>;

