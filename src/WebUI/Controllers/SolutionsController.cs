using AlgoCode.Application.Features.Comments.Commands.PostComment;
using AlgoCode.Application.Features.Solutions.Commands.DeleteSolution;

namespace AlgoCode.WebUI.Controllers
{
    public class SolutionsController : BaseMVCController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PostCommentCommandRequest command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteSolutionCommand{ Id = id });
            return RedirectToAction("Index", "Problems");
        }


    }
}
