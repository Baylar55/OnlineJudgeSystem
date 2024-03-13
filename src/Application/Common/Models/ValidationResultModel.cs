namespace AlgoCode.Application.Common.Models
{
    public class ValidationResultModel
    {
        public bool IsValid { get; set; } = true;
        public Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();
    }
}
