namespace AlgoCode.Application.Common.Interfaces
{
    public interface IErrorHandlingService
    {
        void AddErrorsToModelState(ValidationResultModel validationResult);
    }
}
