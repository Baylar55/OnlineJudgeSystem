using MediatR;

namespace WebUI.Controllers.Base
{
    public class BaseMVCController : Controller
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
