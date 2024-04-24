using AlgoCode.Application.Features.Comments.Commands.PostComment;

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
    }
}
