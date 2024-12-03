namespace AlgoCode.Application.Features.Contests.Commands.CreateContest;

public record CreateContestCommand(string Title, string Description, IFormFile Photo, DateTime StartTime, DateTime EndTime, bool IsActive, List<int> ProblemIds) : IRequest<ValidationResultModel>;
