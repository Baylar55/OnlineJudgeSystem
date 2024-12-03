namespace AlgoCode.Application.Common.Models;

public class ValidationResultModel
{
    private Dictionary<string, List<string>> _errors = [];
    public Dictionary<string, List<string>> Errors
    {
        get => _errors;
        set
        {
            _errors = value ?? [];
            IsValid = _errors.Count == 0;
        }
    }
    public bool IsValid { get; private set; }
}
