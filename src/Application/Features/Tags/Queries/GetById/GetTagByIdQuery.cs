namespace AlgoCode.Application.Features.Tags.Queries.GetById
{
    public class GetTagByIdQuery : IRequest<GetTagByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
