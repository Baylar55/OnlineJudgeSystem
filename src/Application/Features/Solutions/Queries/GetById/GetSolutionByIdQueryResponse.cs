namespace AlgoCode.Application.Features.Solutions.Queries.GetById
{
    public class GetSolutionByIdQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
