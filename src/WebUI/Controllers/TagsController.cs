using AlgoCode.Application.Features.Tags.Commands.CreateTag;
using AlgoCode.Application.Features.Tags.Commands.DeleteTag;
using AlgoCode.Application.Features.Tags.Commands.UpdateTag;
using AlgoCode.Application.Features.Tags.Queries;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    public class TagsController : BaseMVCController
    {
        private readonly IErrorHandlingService _errorHandlingService;
        public TagsController(IErrorHandlingService errorHandlingService)
        {
            _errorHandlingService = errorHandlingService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            var model = await Mediator.Send(new GetTagsWithPaginationQuery());
            return View(model);
        }

        [HttpGet("[action]")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateTagCommand command)
        {
            var validationResult = await Mediator.Send(command);
            if (!validationResult.IsValid)
            {
                _errorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Update(int id)
        {
            var model = await Mediator.Send(new GetTagByIdQuery { Id = id });
            return View(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update(UpdateTagCommand command)
        {
            var validationResult = await Mediator.Send(command);
            if (!validationResult.IsValid)
            {
                _errorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction("Index");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTagCommand { Id = id });
            return RedirectToAction(nameof(Index));
        }
    }
}
