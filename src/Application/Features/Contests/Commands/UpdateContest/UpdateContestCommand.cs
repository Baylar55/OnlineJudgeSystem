using Microsoft.AspNetCore.Http;

namespace AlgoCode.Application.Features.Contests.Commands.UpdateContest
{
    public class UpdateContestCommand : IRequest<ValidationResultModel>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public List<int> ProblemIds { get; set; }
    }
}
