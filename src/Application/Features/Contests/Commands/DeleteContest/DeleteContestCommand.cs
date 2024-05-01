namespace AlgoCode.Application.Features.Contests.Commands.DeleteContest
{
    public class DeleteContestCommand : IRequest<ValidationResultModel>
    {
        public int Id { get; set; }
    }
}
