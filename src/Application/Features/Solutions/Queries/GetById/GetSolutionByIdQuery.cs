namespace AlgoCode.Application.Features.Solutions.Queries.GetById
{
    public class GetSolutionByIdQuery : IRequest<GetSolutionByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
