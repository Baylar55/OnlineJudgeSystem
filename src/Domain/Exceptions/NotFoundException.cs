namespace AlgoCode.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string key, string objectName) : base
        ($"Queried object {objectName} was not found, Key: {key}")
    { }
}
