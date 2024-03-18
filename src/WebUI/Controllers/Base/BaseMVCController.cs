using MediatR;

namespace WebUI.Controllers.Base
{
    public class BaseMVCController : Controller
    {
        private IMediator mediator;
        private IErrorHandlingService errorHandlingService;
        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IErrorHandlingService ErrorHandlingService => errorHandlingService ??= HttpContext.RequestServices.GetService<IErrorHandlingService>();
    }
}
