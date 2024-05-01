using Microsoft.AspNetCore.Http;

namespace AlgoCode.Application.Features.Contests.Commands.CreateContest
{
    public class CreateContestCommand : IRequest<ValidationResultModel>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public List<int> ProblemIds { get; set; }
    }
}
