using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Application.Features.Solutions.Queries.GetAll;

public record GetAllSolutionsByProblemQueryResponse(int Id, string Title, ApplicationUser User, int SubmissionId);
