using AlgoCode.Application.Features.Tags.Commands.CreateTag;
using AlgoCode.Application.Features.Tags.Commands.DeleteTag;
using AlgoCode.Application.Features.Tags.Commands.UpdateTag;
using AlgoCode.Application.Features.Tags.Queries.GetAll;
using AlgoCode.Application.Features.Tags.Queries.GetById;

namespace AlgoCode.WebUI.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("admin/tags")]
    public class TagsController : BaseMVCController
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> Index(int page)
        {
            if (page < 1)
                return RedirectToAction(nameof(Index), new { page = 1 });
            var model = await Mediator.Send(new GetTagsWithPaginationQuery()
            {
                PageNumber = page
            });
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
            if (!ModelState.IsValid)
                return View(command);
            var validationResult = await Mediator.Send(command);
            if (!validationResult.IsValid)
            {
                ErrorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Update(int id)
        {
            var model = await Mediator.Send(new GetTagByIdQuery { Id = id });
            UpdateTagCommand mainmodel = new UpdateTagCommand
            {
                Id = model.Id,
                Title = model.Title
            };
            return View(mainmodel);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update(UpdateTagCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);
            var validationResult = await Mediator.Send(command);
            if (!validationResult.IsValid)
            {
                ErrorHandlingService.AddErrorsToModelState(validationResult);
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
