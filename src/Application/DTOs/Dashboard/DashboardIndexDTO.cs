using AlgoCode.Application.Features.Solutions.Queries.GetAll;

namespace AlgoCode.Application.DTOs.Dashboard
{
	public class DashboardIndexDTO
	{
		public int TotalSolutionsCount { get; set; }
		public int TotalSubmissions { get; set; }
		public int TotalSolvedProblems { get; set; }
		public List<Submission> Submissions { get; set; }
	}
}
